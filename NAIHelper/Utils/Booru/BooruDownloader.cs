using HtmlAgilityPack;
using NAIHelper.Services;
using NAIHelper.ViewModels.UI_Entities;
using Newtonsoft.Json;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NAIHelper.Utils.Extensions;
using Avalonia.Controls.Shapes;
using RestSharp;
using Path = System.IO.Path;
using DynamicData;
using NAIHelper.Utils.Booru.Danbooru;
using NAIHelper.Utils.Booru.GelBooru;
using RestSharp.Authenticators;

namespace NAIHelper.Utils.Booru;

public enum Booru
{
    Danbooru,
    Gelbooru
}
public class BooruDownloader
{
    public static string BaseAddress = "https://danbooru.donmai.us";
    public List<Dir> DirTree = new();
    public static Driver Driver { get; set; }
    public Dictionary<string, Tag> AllTags = new();

    public BooruDownloader()
    {
    }

    #region Download concepts

    public async Task DownloadConcept(string conceptName, Booru booru)
    {
        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        var imagesPath = $"C:\\NAI\\Datasets\\{conceptName}\\training_data\\images\\n_{conceptName}_{booru}\\";
        if (!Directory.Exists(imagesPath))
            Directory.CreateDirectory(imagesPath);
        if (!Directory.Exists($"C:\\NAI\\Datasets\\{conceptName}\\training_data\\regularization_images"))
            Directory.CreateDirectory($"C:\\NAI\\Datasets\\{conceptName}\\training_data\\regularization_images");
        //if (booru == Booru.Danbooru)
        //{
            var posts = await GetConceptPosts(conceptName, booru);
            DownloadImagesAndTags(posts, imagesPath, booru);
        //}
    }

    public async Task<List<BooruEntity>> GetConceptPosts(string conceptName, Booru booru)
    {
        var page     = booru == Booru.Danbooru ? 1 : booru == Booru.Gelbooru ? 0 : 0;
        var postList = new List<BooruEntity>();
        while (true)
        {
            var source = "";
            if (booru == Booru.Danbooru)
                source = $"posts.json?limit=200&tags={conceptName}&page={page}";
            else if (booru == Booru.Gelbooru)
                source = $"index.php?{Settings.GelbooruApiKey}&page=dapi&s=post&q=index&json=1&limit=100&tags={conceptName}&pid={page}";
            var lst = new List<BooruEntity>();
            if (booru == Booru.Danbooru)
                lst = (await g.DanbooruClient.GetAsync<List<DanbooruEntity>>(new RestRequest(source))).Cast<BooruEntity>().Where(_ => !string.IsNullOrEmpty(_.Uri)).ToList();
            else if (booru == Booru.Gelbooru)
            {
                var res = await g.GelbooruClient.GetAsync<GelbooruPosts>(new RestRequest(source));
                if (res.PostList == null)
                    break;
                lst = res.PostList.Cast<BooruEntity>().Where(_ => !string.IsNullOrEmpty(_.Uri)).ToList();
            }

            if (lst.Count == 0)
                break;
            postList.AddRange(lst);
            page+=10;
            Thread.Sleep(1111);
        }
        Thread.Sleep(1111);

        return postList;
    }

    public async void DownloadImagesAndTags(List<BooruEntity> postList, string imagesPath, Booru booru)
    {
        var existFiles = Directory.GetFiles(imagesPath).Select(Path.GetFileNameWithoutExtension).ToList();
        var errorIdList = new List<int>();
        foreach (var x in postList)
        {
            try
            {
                if (existFiles.Contains(x.Id.ToString()))
                    continue;
                var             req    = new RestRequest(x.UriPath);
                await using var fs     = File.OpenWrite($"{imagesPath}\\{x.Id}.{x.Ext}");
                Stream?          stream = null;
                if (booru == Booru.Danbooru)
                    stream = await g.CdnDanbooruClient.DownloadStreamAsync(req);
                else if (booru == Booru.Gelbooru)
                    stream = await g.CdnGelbooruClient.DownloadStreamAsync(req);
                if (stream == null)
                    throw new Exception("null stream");
                await stream.CopyToAsync(fs);
                await File.WriteAllTextAsync($"{imagesPath}\\{x.Id}.txt", x.FormattedTags);

            }
            catch (Exception e)
            {
                errorIdList.Add(x.Id);
            }
        }
    }

    #endregion

    #region Download tags

    public async void DownloadFromFile()
    {
        var lst = JsonConvert.DeserializeObject<List<Dir>>(await File.ReadAllTextAsync("DirTree.json"));
        var dirs = new List<Dir>();
        var tags = new List<Tag>();
        foreach (var x in lst)
        {
            dirs.Add(x);
            GetDirsTagsRec(dirs, tags, x);
        }

        var service = new DirService();
        await service.Create(lst);
    }

    private void GetDirsTagsRec(List<Dir> dirs, List<Tag> tags, Dir dir)
    {
        dirs.AddRange(dir.Dirs);
        tags.AddRange(dir.Tags);
        foreach (var x in dir.Dirs)
            GetDirsTagsRec(dirs, tags, x);
    }

    public void DownloadFromSite()
    {
        //Driver = new Driver();
        //Driver.Init();
        DownloadAll();
    }

    public async void DownloadAll()
    {

        DirTree = DownloadDirs().ToList();
        LoadTagsForDirTree();
        //Driver.Chrome.Quit();
        var dirs = new List<Dir>();
        var tags = new List<Tag>();
        foreach (var x in DirTree)
        {
            dirs.Add(x);
            GetDirsTagsRec(dirs, tags, x);
        }

        JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
        {
            MaxDepth = 1024,
            TypeNameHandling = TypeNameHandling.None,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            NullValueHandling = NullValueHandling.Ignore
        };

        await File.WriteAllTextAsync("DirTree.json", JsonConvert.SerializeObject(DirTree));
        var service = new DirService();
        await service.Create(DirTree);
    }

    private IEnumerable<HtmlNode> DownloadUls(HtmlNode wikibody)
    {
        return wikibody.ChildNodes.Where(x => x.Name.ToLower() == "ul");
    }

    private HtmlNode GetWikiBody(string path)
    {
        Thread.Sleep(1111);
        Driver.Chrome.Navigate().GoToUrl($"{BaseAddress}/{path}");
        Driver.WaitExist("//*[@id=\"c-wiki-pages\"]");
        var html = Driver.Chrome.FindElement(By.TagName("Body")).GetAttribute("innerHTML");
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        var wikibody = doc.GetElementbyId("wiki-page-body");
        return wikibody;
    }

    public IList<Dir> DownloadDirs()
    {
        var uls = DownloadUls(GetWikiBody("wiki_pages/tag_groups"));
        var dirsTree = new List<Dir>();
        foreach (var ul in uls)
        {
            if (ul.PreviousSibling.InnerText.ToLower().Contains("see also"))
                continue;
            if (ul.PreviousSibling.InnerText.ToLower().Contains("characters"))
                continue;
            var dir = new Dir(ul.PreviousSibling.InnerText.Trim());
            if (dir.Name.ToLower().Contains("tag group:"))
                dir.Name = dir.Name.Remove(0, "tag group:".Length);
            dir.Name = dir.Name.Trim().FirstCharToUpper();
            LoadDirChilds(dir, ul);
            dirsTree.Add(dir);
        }

        return dirsTree;
    }

    private void LoadDirChilds(Dir dir, HtmlNode node)
    {
        foreach (var child in node.ChildNodes)
            if (child.Name.ToLower() == "li")
            {
                var aNode = child.ChildNodes.FirstOrDefault(x => x.Name.ToLower() == "a");
                if (aNode != null)
                {
                    var newDir = new Dir();
                    if (aNode.InnerText.Trim().ToLower().Contains("tag group:"))
                    {
                        newDir.Name = aNode.InnerText.Remove(0, "tag group:".Length);
                    }
                    else if (aNode.InnerText.Trim().ToLower().Contains("list of "))
                    {
                        newDir.Name = aNode.InnerText.Remove(0, "list of ".Length);
                    }
                    else
                    {
                        newDir.Name = aNode.InnerText.Trim();

                        var tagName = aNode.InnerText.Trim().FirstCharToUpper();
                        if (AllTags.TryGetValue(tagName, out var tag))
                        {
                            tag.Dirs.Add(newDir);
                        }
                        else
                        {
                            var newTag = new Tag(tagName, aNode.GetAttributeValue("href", null));
                            newTag.Dirs.Add(newDir);
                            newDir.Tags.Add(newTag);
                            AllTags.Add(newTag.Name, newTag);
                        }

                        //dir.Tags.Add(new Tag(aNode.InnerText.Trim().FirstCharToUpper(), aNode.GetAttributeValue("href", null)));
                    }

                    newDir.Link = aNode.GetAttributeValue("href", null);
                    dir.Name = dir.Name.Trim().FirstCharToUpper();
                    dir.Dirs.Add(newDir);
                }
            }
            else if (child.Name.ToLower() == "ul")
            {
                LoadDirChilds(dir.Dirs.Last(), child);
            }
            else
            {
            }
    }

    private void LoadTagsForDirTree()
    {
        foreach (var dir in DirTree) //.Where(x => x.Name.ToLower() == "body"))
            LoadTagsForDirTree(dir);
    }

    private void LoadTagsForDirTree(Dir dir)
    {
        if (!string.IsNullOrEmpty(dir.Link))
        {
            var wikibody = GetWikiBody(dir.Link);
            var nodes = wikibody.ChildNodes.Where(x => x.Name.ToLower().StartsWith("h") && x.Name.Length == 2 || x.Name.ToLower() == "ul").ToList();
            var excludeNode = GetExcludeNode(nodes);
            while (excludeNode != null)
            {
                var pos = nodes.IndexOf(excludeNode);
                var endpos = pos + 1;
                while (endpos < nodes.Count && nodes[endpos].Name.ToLower().StartsWith("h") && nodes[endpos].Name.Length == 2)
                    endpos++;
                while (endpos < nodes.Count && nodes[endpos].Name.ToLower() == "ul")
                    endpos++;
                var cnt = endpos - pos;
                for (var i = 0; i < cnt; i++)
                    nodes.RemoveAt(pos);
                excludeNode = GetExcludeNode(nodes);
            }

            (Dir dir, int level) currentParent = (dir, 0);
            var parentList = new List<(Dir dir, int level)> { currentParent };
            foreach (var node in nodes)
                if (node.Name.ToLower().StartsWith("h"))
                {
                    var level = int.Parse(node.Name.Substring(1, 1));
                    if (level > currentParent.level)
                    {
                        var newDir = new Dir(node.InnerText.Trim().FirstCharToUpper());
                        currentParent.dir.Dirs.Add(newDir);
                        parentList.Add((newDir, level));
                        currentParent = parentList.Last();
                    }
                    else if (level == currentParent.level)
                    {
                        parentList.Remove(currentParent);
                        currentParent = parentList.Last();
                        var newDir = new Dir(node.InnerText.Trim().FirstCharToUpper());
                        currentParent.dir.Dirs.Add(newDir);
                        parentList.Add((newDir, level));
                        currentParent = parentList.Last();
                    }
                    else
                    {
                        while (level < currentParent.level)
                        {
                            parentList.Remove(currentParent);
                            currentParent = parentList.Last();
                        }

                        var newDir = new Dir(node.InnerText.Trim().FirstCharToUpper());
                        currentParent.dir.Dirs.Add(newDir);
                        parentList.Add((newDir, level));
                        currentParent = parentList.Last();
                    }
                }
                else if (node.Name.ToLower() == "ul")
                {
                    var lis = node.Descendants("li").Where(x => x.ChildNodes.Any(c => c.Name.ToLower() == "a")).ToList();
                    foreach (var x in lis)
                        foreach (var c in x.ChildNodes.Where(c => c.Name.ToLower() == "a"))
                            if (!x.InnerText.ToLower().StartsWith("tag group:"))
                            {
                                var tagName = c.InnerText.Trim().FirstCharToUpper();
                                if (AllTags.TryGetValue(tagName, out var tag))
                                {
                                    tag.Dirs.Add(currentParent.dir);
                                }
                                else
                                {
                                    var newTag = new Tag(tagName, c.GetAttributeValue("href", null));
                                    newTag.Dirs.Add(currentParent.dir);
                                    currentParent.dir.Tags.Add(newTag);
                                    AllTags.Add(newTag.Name, newTag);
                                }
                            }
                }
                else
                {
                    throw new Exception();
                }

            foreach (var child in dir.Dirs)
                LoadTagsForDirTree(child);
        }
        else
        {
            foreach (var child in dir.Dirs)
                LoadTagsForDirTree(child);
        }
    }

    public static HtmlNode? GetExcludeNode(IList<HtmlNode> nodes)
    {
        return nodes.FirstOrDefault(x => x.Name.ToLower().StartsWith("h") && x.Name.Length == 2 &&
                                         (x.InnerText.ToLower() == "related tags" || x.InnerText.ToLower() == "Related tag groups".ToLower() || x.InnerText.ToLower() == "see also" ||
                                          x.InnerText.ToLower() == "External links".ToLower()));
    }

    #endregion
}
