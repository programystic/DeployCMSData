﻿using System.Collections.Generic;
using System.Linq;

namespace DeployCmsData.UmbracoCms.Models
{

    public class GridItemsPreValue
    {        
        public List<Style> Styles { get; set; }
        public List<Config> Config { get; set; }
        public int Columns { get; set; }
        public List<Template> Templates { get; set; }
        public List<Layout> Layouts { get; set; }

        public GridItemsPreValue()
        {
            Styles = new List<Style>();
            Config = new List<Config>();
            Templates = new List<Template>();
            Layouts = new List<Layout>();
        }
    }

    public class Style
    {
        public string Label { get; set; }
        public string Description { get; set; }
        public string Key { get; set; }
        public string View { get; set; }
        public string Modifier { get; set; }
    }

    public class Config
    {
        public string Label { get; set; }
        public string Description { get; set; }
        public string Key { get; set; }
        public string View { get; set; }
    }

    public class Section
    {
        public int Grid { get; set; }

        public Section (int gridValue)
        {
            Grid = gridValue;
        }        
    }

    public class Template
    {
        public string Name { get; set; }
        public List<Section> Sections { get; set; }

        public Template()
        {
            Sections = new List<Section>();
        }
    }

    public class Area
    {
        public int Grid { get; set; }
        public List<string> Editors { get; set; }

        public Area(int grid)
        {
            Grid = grid;
        }

        public Area(int grid, params string[] editors)
        {
            Grid = grid;
            Editors = editors.ToList();
        }
    }

    public class Layout
    {
        public string Label { get; set; }
        public string Name { get; set; }
        public List<Area> Areas { get; set; }

        public Layout()
        {
            Areas = new List<Area>();
        }
    } 
}