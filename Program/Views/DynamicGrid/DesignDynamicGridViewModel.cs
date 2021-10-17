﻿using System.Collections.ObjectModel;
using System.Windows.Media;
using MEATaste.Views.Controls;
using MEATaste.Views.Controls.Models;

// adapted from Illya Reznykov https://github.com/IReznykov/Blog

namespace MEATaste.Views.DynamicGrid
{
    internal class DesignDynamicGridViewModel : IDynamicGridViewModel
    {
        public int ViewWidth { get; set; } = 300;
        public int ViewHeight { get; set; } = 200;
        public int CellWidth { get; set; } = 20;
        public int CellHeight { get; set; } = 20;
        public CellSet CellSet { get; } = new CellSet(15, 10);
        public ObservableCollection<ObservableCollection<ICellViewModel>> Cells { get; } = null;
        public int GridWidth { get; } = 15;
        public int GridHeight { get; } = 10;
        public Color StartColor { get; set; } = Colors.AliceBlue;
        public Color FinishColor { get; set; } = Colors.DarkBlue;
        public Color BorderColor { get; set; } = Colors.DarkGray;

    }
}
