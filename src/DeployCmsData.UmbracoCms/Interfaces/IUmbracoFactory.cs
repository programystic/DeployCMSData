﻿using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Core.Models.Entities;

namespace DeployCmsData.UmbracoCms.Interfaces
{
    public interface IUmbracoFactory
    {
        IContentType NewContentType(int parentId);
        IUmbracoEntity NewContainer(int parentId, string name, int parentLevel);
        IUmbracoEntity GetContainer(string name, int level);
        PropertyType NewPropertyType(IDataType dataTypeDefinition, string propertyAlias);
        IEnumerable<IContent> GetChildren(IContent content);
        ITemplate NewTemplate(string name, string templateAlias);
    }
}