using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Application.Dto.Base
{
    public class DtoBase
    {
        private readonly List<Link> m_links = new List<Link>();

        [JsonProperty(Order = 100)]
        public IEnumerable<Link> Links { get { return m_links; } }

        public void AddLink(Link a_link)
        {
           m_links.Add(a_link);
        }

        public void AddLinks(params Link[] a_links)
        {
           Array.ForEach(a_links, AddLink);
        }
    }
}