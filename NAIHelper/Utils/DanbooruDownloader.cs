using HtmlAgilityPack;
using NAIHelper.Services;
using NAIHelper.ViewModels.UI_Entities;
using Newtonsoft.Json;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NAIHelper.Utils
{
    public class DanbooruDownloader
    {
        public static string    BaseAddress = "https://danbooru.donmai.us";
        public        List<Dir> DirTree     = new();

        public DanbooruDownloader()
        {
            g.Driver = new Driver();
            g.Driver.Init();
            DownloadAll();
            g.Driver.Chrome.Quit();
        }

        public async void DownloadAll()
        {
            DirTree = DownloadDirs().ToList();
            LoadTagsForDirTree();
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
            Thread.Sleep(3000);
            g.Driver.Chrome.Navigate().GoToUrl($"{BaseAddress}/{path}");
            g.Driver.WaitExist("//*[@id=\"c-wiki-pages\"]");
            var html = g.Driver.Chrome.FindElement(By.TagName("Body")).GetAttribute("innerHTML");
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
                if (ul.PreviousSibling.InnerText.ToLower().Contains("see also")) continue;
                if (ul.PreviousSibling.InnerText.ToLower().Contains("characters")) continue;
                var dir = new Dir(ul.PreviousSibling.InnerText);
                if (dir.Name.ToLower().Contains("tag group:"))
                    dir.Name = dir.Name.Remove(0, "tag group:".Length);
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
                        if (aNode.InnerText.ToLower().Contains("tag group:"))
                        {
                            newDir.Name = aNode.InnerText.Remove(0, "tag group:".Length);
                        }
                        else if (aNode.InnerText.ToLower().Contains("list of "))
                        {
                            newDir.Name = aNode.InnerText.Remove(0, "list of ".Length);
                        }
                        else
                        {
                            newDir.Name = aNode.InnerText;
                            dir.Tags.Add(new Tag(aNode.InnerText, aNode.GetAttributeValue("href", null)));
                        }

                        newDir.Link = aNode.GetAttributeValue("href", null);
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
                var nodes = wikibody.ChildNodes.Where(x => (x.Name.ToLower().StartsWith("h") && x.Name.Length == 2) || x.Name.ToLower() == "ul").ToList();
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
                            var newDir = new Dir(node.InnerText);
                            currentParent.dir.Dirs.Add(newDir);
                            parentList.Add((newDir, level));
                            currentParent = parentList.Last();
                        }
                        else if (level == currentParent.level)
                        {
                            parentList.Remove(currentParent);
                            currentParent = parentList.Last();
                            var newDir = new Dir(node.InnerText);
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

                            var newDir = new Dir(node.InnerText);
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
                                    currentParent.dir.Tags.Add(new Tag(c.InnerText, c.GetAttributeValue("href", null)));
                    }
                    else
                    {
                        throw new Exception();
                    }

                foreach (var child in dir.Dirs) LoadTagsForDirTree(child);
            }
            else
            {
                foreach (var child in dir.Dirs) LoadTagsForDirTree(child);
            }
        }
        public static HtmlNode? GetExcludeNode(IList<HtmlNode> nodes)
        {
            return nodes.FirstOrDefault(x => x.Name.ToLower().StartsWith("h") && x.Name.Length == 2 &&
                                             (x.InnerText.ToLower() == "related tags" || x.InnerText.ToLower() == "Related tag groups".ToLower() || x.InnerText.ToLower() == "see also" ||
                                              x.InnerText.ToLower() == "External links".ToLower()));
        }
    }
}
