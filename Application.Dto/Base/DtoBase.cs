using System;
using System.Collections.Generic;
using Infrastructure;
using Newtonsoft.Json;

namespace Application.Dto.Base
{
    public class DtoBase
    {
       private readonly List<ILink> m_links = new List<ILink>();

        [JsonProperty(Order = 100)]
        public IList<ILink> Links { get { return m_links; } }

        public void AddLink(ILink a_link)
        {
           m_links.Add(a_link);
        }

        public void AddLinks(params ILink[] a_links)
        {
           Array.ForEach(a_links, AddLink);
        }
    }
}