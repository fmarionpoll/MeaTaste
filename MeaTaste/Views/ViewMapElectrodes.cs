﻿using Microsoft.Win32;
using System.Windows;
using MeaTaste.DataMEA.Models;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;
using TasteMEA.DataMEA.Utilies;
using ScottPlot.WPF;
using System.Drawing;

namespace MeaTaste
{
    public partial class MainWindow : Window
    {

        public ScottPlot.Plottable.ScatterPlot HighlightedPoint;
        public int LastHighlightedIndex = -1;

       

        private void InitElectrodesMap()
        {
            double[] xs = MeaExpInfos.Descriptors.GetArray_electrodes_XPos();
            double[] ys = MeaExpInfos.Descriptors.GetArray_electrodes_YPos();
            var plt = WpfElectrodesMap.Plot;
            plt.AddScatterPoints(xs, ys);
            plt.Title("Electrodes Map");
            plt.XLabel("Horizontal position µm");
            plt.YLabel("Vertical position µm");

            // Add a red circle we can move around later as a highlighted point indicator
            HighlightedPoint = plt.AddPoint(0, 0);
            HighlightedPoint.Color = Color.Red;
            HighlightedPoint.MarkerSize = 10;
            HighlightedPoint.MarkerShape = ScottPlot.MarkerShape.openCircle;
            HighlightedPoint.IsVisible = false;
        }

        private void UpdateSelectedElectrodeOnMap(Electrode electrode)
        {
            HighLightMapOfElectrodeAt(electrode.XCoordinates_um, electrode.YCoordinates_um, electrode.ChannelNumber);
        }

        private void HighLightMapOfElectrodeAt(double xs, double ys, int pointIndex)
        {
            // place the highlight over the point of interest
            HighlightedPoint.Xs[0] = xs;
            HighlightedPoint.Ys[0] = ys;
            HighlightedPoint.IsVisible = true;

            // render if the highlighted point chnaged
            if (LastHighlightedIndex != pointIndex)
            {
                LastHighlightedIndex = pointIndex;
                WpfElectrodesMap.Plot.Render();
            }
        }

        private void WpfElectrodesMap_MouseMove(object sender, MouseEventArgs e)
        {
            int pointIndex = MoveCursorNearPoint(sender);
        }

        private void WpfElectrodesMap_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int pointIndex = MoveCursorNearPoint(sender);
            Electrode electrode = MeaExpInfos.Descriptors.Electrodes[pointIndex];
            // TODO: not sure this is right (search item first then select?)
            UpdateSelectedElectrode(electrode);
            ListViewChannels.SelectedIndex = pointIndex;
        }

        private int MoveCursorNearPoint(object sender)
        {
            ScottPlot.WpfPlot changedPlot = (ScottPlot.WpfPlot)sender;
            (double mouseCoordX, double mouseCoordY) = changedPlot.GetMouseCoordinates();
            double xyRatio = changedPlot.Plot.XAxis.Dims.PxPerUnit / changedPlot.Plot.YAxis.Dims.PxPerUnit;

            int index = 0;
            ScottPlot.Plottable.IPlottable[] table = WpfElectrodesMap.Plot.GetPlottables();
            if (table.Length > 0)
            {
                ScottPlot.Plottable.ScatterPlot MyScatterPlot = (ScottPlot.Plottable.ScatterPlot)table[0];
                (double pointX, double pointY, int pointIndex) = MyScatterPlot.GetPointNearest(mouseCoordX, mouseCoordY, xyRatio);
                HighLightMapOfElectrodeAt(pointX, pointY, pointIndex);
                index = pointIndex;
            }
            return index;
        }

    }


}