using System.Collections.Generic;

namespace ApexCharts
{
    /// <summary>
    /// Return data sent when <see cref="ApexChart{TItem}.OnMarkerClick"/> or <see cref="ApexChart{TItem}.OnDataPointSelection"/> are invoked
    /// </summary>
    /// <typeparam name="TItem">The data type of the items to display in the chart</typeparam>
    public class SelectedData<TItem> where TItem : class
    {
        /// <summary>
        /// A reference to the chart where a selection has occurred
        /// </summary>
        public ApexChart<TItem> Chart { get; set; }

        /// <summary>
        /// The series containing the data point that has been selected
        /// </summary>
        public Series<TItem> Series { get; set; }

        /// <summary>
        /// The data point that has been selected
        /// </summary>
        public IDataPoint<TItem> DataPoint { get; set; }

        /// <summary>
        /// Specifies whether the data point is currently selected
        /// </summary>
        public bool IsSelected { get; internal set; }

        /// <summary>
        /// The index of the data point being selected
        /// </summary>
        public int DataPointIndex { get; set; }

        /// <summary>
        /// The index of the data series being selected
        /// </summary>
        public int SeriesIndex { get; set; }
    }

    /// <summary>
    /// Return data sent when <see cref="ApexChart{TItem}.OnDataPointEnter"/> or <see cref="ApexChart{TItem}.OnDataPointLeave"/> are invoked. Additionally used for the context object within <see cref="ApexChart{TItem}.ApexPointTooltip"/>.
    /// </summary>
    /// <typeparam name="TItem">The data type of the items to display in the chart</typeparam>
    public class HoverData<TItem> where TItem : class
    {
        /// <summary>
        /// A reference to the chart where a hover action has occurred
        /// </summary>
        public ApexChart<TItem> Chart { get; set; }

        /// <summary>
        /// The series containing the data point that has been hovered
        /// </summary>
        public Series<TItem> Series { get; set; }

        /// <summary>
        /// The data point that has been hovered
        /// </summary>
        public IDataPoint<TItem> DataPoint { get; set; }

        /// <summary>
        /// The index of the data point being hovered
        /// </summary>
        public int DataPointIndex { get; set; }

        /// <summary>
        /// The index of the data series being hovered
        /// </summary>
        public int SeriesIndex { get; set; }
    }

    /// <summary>
    /// Return data sent when <see cref="ApexChart{TItem}.OnZoomed"/> is invoked
    /// </summary>
    /// <typeparam name="TItem">The data type of the items to display in the chart</typeparam>
    public class ZoomedData<TItem> where TItem : class
    {
        /// <summary>
        /// A reference to the chart where a zoom action has been performed
        /// </summary>
        public ApexChart<TItem> Chart { get; set; }

        /// <inheritdoc cref="SelectionXAxis"/>
        public SelectionXAxis XAxis { get; set; }

        /// <summary>
        /// Y Axis objects
        /// </summary>
        public List<object> YAxis { get; set; }

        /// <summary>
        /// Specifies whether there is zoom applied to the chart
        /// </summary>
        public bool IsZoomed => XAxis?.Min != null && XAxis?.Max != null;
    }

    /// <summary>
    /// Return data sent when <see cref="ApexChart{TItem}.OnSelection"/> or <see cref="ApexChart{TItem}.OnBrushScrolled"/> are invoked
    /// </summary>
    /// <typeparam name="TItem">The data type of the items to display in the chart</typeparam>
    public class SelectionData<TItem> where TItem : class
    {
        /// <summary>
        /// A reference to the chart where data has been selected
        /// </summary>
        public ApexChart<TItem> Chart { get; set; }

        /// <inheritdoc cref="SelectionXAxis"/>
        public SelectionXAxis XAxis { get; set; }

        /// <inheritdoc cref="SelectionYAxis"/>
        public SelectionYAxis YAxis { get; set; }
    }

    /// <summary>
    /// Contains details related to the X-axis values that have been selected
    /// </summary>
    public class SelectionXAxis
    {
        /// <summary>
        /// The minimum X-axis value for data that has been selected
        /// </summary>
        public decimal? Min { get; set; }

        /// <summary>
        /// The maximum X-axis value for data that has been selected
        /// </summary>
        public decimal? Max { get; set; }
    }

    /// <summary>
    /// Contains details related to the Y-axis values that have been selected
    /// </summary>
    public class SelectionYAxis
    {
        /// <summary>
        /// The minimum Y-axis value for data that has been selected
        /// </summary>
        public decimal? Min { get; set; }

        /// <summary>
        /// The maximum Y-axis value for data that has been selected
        /// </summary>
        public decimal? Max { get; set; }
    }


    /// <summary>
    /// Return data sent when <see cref="ApexChart{TItem}.OnSelection"/> or <see cref="ApexChart{TItem}.OnBrushScrolled"/> are invoked
    /// </summary>
    /// <typeparam name="TItem">The data type of the items to display in the chart</typeparam>
    public class AnnotationEvent<TItem> where TItem : class
    {
        /// <summary>
        /// A reference to the chart where data has been selected
        /// </summary>
        public ApexChart<TItem> Chart { get; set; }

        /// <summary>
        /// The Id of the annotation
        /// </summary>
        public string AnnotationId { get; set; }

        /// <summary>
        /// The event Target
        /// </summary>
        public AnnotationEventTarget Target { get; set; }

        /// <summary>
        /// The type of event
        /// </summary>
        public AnnotationEventType EventType { get; set; }

        /// <summary>
        /// Annotation where the event occurd
        /// </summary>
        public IAnnotation Annotation { get; set; }

    }

    /// <summary>
    /// Indicates the Event type
    /// </summary>
    public enum AnnotationEventTarget
    {
        /// <summary>
        /// Target Label
        /// </summary>
        Label,

        /// <summary>
        /// Target Point
        /// </summary>
        Point,


    }

    /// <summary>
    /// Indicates the Event type
    /// </summary>
    public enum AnnotationEventType
    {
        /// <summary>
        /// Click Event
        /// </summary>
        Click,
        
        /// <summary>
        /// Mouse Leave
        /// </summary>
        MouseLeave,

       /// <summary>
       /// Mouse Enter
       /// </summary>
        MouseEnter,
    }
}
