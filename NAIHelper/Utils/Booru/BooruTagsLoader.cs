using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using NAIHelper.Utils.Extensions;
using NAIHelper.ViewModels;
using NAIHelper.ViewModels.UI_Entities;
using RestSharp;
using ReactiveUI;

namespace NAIHelper.Utils.Booru
{
    public class BooruTagsLoader : ViewModelBase
    {
        private int _grabbedLinksCount;
        public int GrabbedLinksCount
        {
            get => _grabbedLinksCount;
            set => this.RaiseAndSetIfChanged(ref _grabbedLinksCount, value);
        }

        private int _downloadProgress;
        public int DownloadProgress
        {
            get => _downloadProgress;
            set => this.RaiseAndSetIfChanged(ref _downloadProgress, value);
        }

        private readonly Dictionary<string, Tag> _allTags = new();

        public BooruTagsLoader()
        {
            
        }

        public async Task<List<Dir>> DownloadTree()
        {
            var dirs = await DownloadDirs();
            foreach (var x in dirs)
                await LoadTagsTree(x);
            return dirs;
        }

        private async Task<List<Dir>> DownloadDirs()
        {
            var wiki = await DownloadWikiBody("wiki_pages/tag_groups.html");
            var uls  = wiki.ChildNodes.Where(_ => _.Name.ToLower() == "ul");
            var tree = new List<Dir>();
            foreach (var x in uls)
            {
                var prevName = x.PreviousSibling.InnerText.ToLower().Trim();
                if(prevName.Contains("see also") || prevName.Contains("characters")) continue;
                if (prevName.Contains("tag group:"))
                    prevName = prevName.Replace("tag group:", "").Trim();
                var dir = new Dir(prevName.FirstCharToUpper());
                await LoadDirChilds(dir, x);
                tree.Add(dir);
            }

            GrabbedLinksCount = tree.GetAllNodesCount();
            return tree;
        }

        private async Task<HtmlNode> DownloadWikiBody(string path)
        {
            var resp = await g.DanbooruClient.ExecuteAsync(new RestRequest(path));
            if (!resp.IsSuccessStatusCode || string.IsNullOrEmpty(resp.Content))
                throw new Exception($"DownloadWikiBody:{path} download error");
            var doc = new HtmlDocument();
            doc.LoadHtml(resp.Content);
            var wiki = doc.GetElementbyId("wiki-page-body");
            if(wiki == null)
                throw new Exception($"DownloadWikiBody:{path} get wiki-page-body error");
            return wiki;
        }

        private async Task LoadDirChilds(Dir dir, HtmlNode node)
        {
            foreach (var child in node.ChildNodes)
            {
                if (child.Name.ToLower() == "li")
                {
                    var aNode = child.ChildNodes.FirstOrDefault(x => x.Name.ToLower() == "a");
                    if (aNode != null)
                    {
                        var newDir  = new Dir();
                        var dirName = aNode.InnerText.Trim().ToLower();
                        if (dirName.Contains("tag group:"))
                        {
                            dirName = dirName.Replace("tag group:", "").Trim();
                        }
                        else if (dirName.Contains("list of "))
                        {
                            dirName = dirName.Replace("list of ", "").Trim();
                        }
                        else
                        {
                            var tagName = aNode.InnerText.ToLower().Trim().FirstCharToUpper();
                            if (_allTags.TryGetValue(tagName, out var tag))
                            {
                                tag.Dirs.Add(newDir);
                            }
                            else
                            {
                                var newTag = new Tag(tagName, aNode.GetAttributeValue("href", null));
                                newTag.Dirs.Add(newDir);
                                newDir.Tags.Add(newTag);
                                _allTags.Add(newTag.Name, newTag);
                            }
                        }

                        newDir.Name = dirName.FirstCharToUpper();
                        newDir.Link = aNode.GetAttributeValue("href", null);
                        dir.Dirs.Add(newDir);
                    }
                }
                else if (child.Name.ToLower() == "ul")
                {
                    await LoadDirChilds(dir.Dirs.Last(), child);
                }
                else
                {
                }
            }
        }

        private async Task LoadTagsTree(Dir dir)
        {
            if (!string.IsNullOrEmpty(dir.Link))
            {
                var wikibody = await DownloadWikiBody(dir.Link);
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
                                    if (_allTags.TryGetValue(tagName, out var tag))
                                    {
                                        tag.Dirs.Add(currentParent.dir);
                                    }
                                    else
                                    {
                                        var newTag = new Tag(tagName, c.GetAttributeValue("href", null));
                                        newTag.Dirs.Add(currentParent.dir);
                                        currentParent.dir.Tags.Add(newTag);
                                        _allTags.Add(newTag.Name, newTag);
                                    }
                                }
                    }
                    else
                    {
                        throw new Exception();
                    }

                DownloadProgress++;

                foreach (var child in dir.Dirs)
                    await LoadTagsTree(child);
            }
            else
            {
                foreach (var child in dir.Dirs)
                    await LoadTagsTree(child);
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
