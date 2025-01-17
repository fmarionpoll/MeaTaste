﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using MEATaste.Annotations;
using OxyPlot;


namespace MEATaste.Views.MapElectrodes
{
    public class MapElectrodesModel : INotifyPropertyChanged
    {
        private PlotModel scatterPlotModel;

        public PlotModel ScatterPlotModel
        {
            get => scatterPlotModel;
            set
            {
                if (scatterPlotModel == value) return;
                scatterPlotModel = value;
                OnPropertyChanged(nameof(ScatterPlotModel));
            }
        }

        public MapElectrodesModel()
        {
            ScatterPlotModel = new PlotModel();
            ScatterPlotModel.Axes.Add(new OxyPlot.Axes.LinearAxis { Position = OxyPlot.Axes.AxisPosition.Bottom });
            ScatterPlotModel.Axes.Add(new OxyPlot.Axes.LinearAxis { Position = OxyPlot.Axes.AxisPosition.Left, Maximum = 10, Minimum = 0 });
        }

       public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
