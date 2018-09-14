using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dollar.One;
[RequireComponent(typeof(LineRenderer))]
public class HandwritingPointsManager : MonoBehaviour {

    public OneDollar oneDollar;
    public RectTransform drawingImage;
    public Text resultLabel;
    public LineRenderer bestMatchLineRenderer;
    public LineRenderer evalLineRenderer;
    public LineRenderer resampleLineRenderer;
    public Vector2 bestMatchLineRendererOffset;
    public Vector2 evalLineRendererOffset;

    public float intervalDistance;

    Vector2 prevPointRegistered;
    public List<Vector2> pointList;
    public List<Vector3> worldPointList;
    bool registering = false;

    LineRenderer line;

	void Start () {

        line = GetComponent<LineRenderer>();
    }

    #region Event APIs
    public void OnInitDrawing(Vector2 point, Vector3 worldPoint)
    {
        if (registering) return;

        ClearPointList();
        registering = true;
        AddPoint(point, worldPoint);
        prevPointRegistered = point;

        resultLabel.text = "Drawing...";

        bestMatchLineRenderer.positionCount = 0;
        evalLineRenderer.positionCount = 0;
    }
    public void OnDrawing(Vector2 point, Vector3 worldPoint)
    {
        if (!registering) return;

        if (Vector2.Distance(point, prevPointRegistered) < intervalDistance) return;

        AddPoint(point, worldPoint);
        prevPointRegistered = point;

        UpdateDraw();
        
    }
    public void OnEndDrawing(Vector2 point, Vector3 worldPoint)
    {
        pointList.Add(point);
        registering = false;
        Point[] points = VecToPoint(pointList);

        Unistroke resampleStroke = new Unistroke(points, EvaluationType.ResampledOnly);
        Vector3[] plot = new Vector3[resampleStroke.Points.Length];
        for (int i = 0; i < resampleStroke.Points.Length; i++)
        {
            plot[i] = Camera.main.ScreenToWorldPoint(new Vector3(resampleStroke.Points[i].X + bestMatchLineRendererOffset.x, resampleStroke.Points[i].Y + bestMatchLineRendererOffset.y, 9f));
        }
        resampleLineRenderer.positionCount = plot.Length;
        resampleLineRenderer.SetPositions(plot);



        Result result = oneDollar.Recognize(points);
        string msg = "Match:" + result.Name + "\nvariant:" + result.Variant + "\ntest:" + result.Test + "\nscore:" + result.Score + "\ntime:" + result.Time;
        resultLabel.text = msg;
        //Debug.Log(result.Name + " | variant:" + result.Variant + " | test:" + result.Test + " | score:" + result.Score + " | time:" + result.Time);
        if(bestMatchLineRenderer != null && result.Points != null)
        {
            Unistroke protractoredStroke = new Unistroke(result.Points, EvaluationType.ProportionateProtractor);
            Vector3[] plotPoints = new Vector3[protractoredStroke.Points.Length];
            for(int i = 0; i < plotPoints.Length; i++)
            {
                plotPoints[i] = Camera.main.ScreenToWorldPoint( new Vector3(protractoredStroke.Points[i].X + bestMatchLineRendererOffset.x, protractoredStroke.Points[i].Y + bestMatchLineRendererOffset.y, 9f) );
            }
            bestMatchLineRenderer.positionCount = plotPoints.Length;
            bestMatchLineRenderer.SetPositions(plotPoints);
        }

        if (evalLineRenderer != null)
        {
            Unistroke protractoredStroke = new Unistroke(points, EvaluationType.ProportionateProtractor);
            Vector3[] plotPoints = new Vector3[protractoredStroke.Points.Length];
            for (int i = 0; i < plotPoints.Length; i++)
            {
                plotPoints[i] = Camera.main.ScreenToWorldPoint(new Vector3(protractoredStroke.Points[i].X + evalLineRendererOffset.x, protractoredStroke.Points[i].Y + evalLineRendererOffset.y, 9f));
            }
            evalLineRenderer.positionCount = plotPoints.Length;
            evalLineRenderer.SetPositions(plotPoints);
        }

    }
    #endregion

    #region guiAPIs
    public void AddGestureToPositiveList()
    {
        Point[] points = VecToPoint(pointList);
        oneDollar.AddGesture(points, StrokeType.test_positive, EvaluationType.None); // add RAW points
        //Debug.Log("PositiveList Length: " + oneDollar.model.positiveList.Count);

    }

    public void AddGestureToNegativeList()
    {
        Point[] points = VecToPoint(pointList);
        oneDollar.AddGesture(points, StrokeType.test_negative, EvaluationType.None);
        //Debug.Log("NegativeList Length: " + oneDollar.model.negativeList.Count);
    }

    public void ResetPositiveSet()
    {
        oneDollar.DeleteAllGestures(StrokeType.test_positive);
    }
    public void ResetNegativeSet()
    {
        oneDollar.DeleteAllGestures(StrokeType.test_negative);
    }
    #endregion

    #region Private functions

    private Point[] VecToPoint(List<Vector2> vectors)
    {
        Point[] result = new Point[vectors.Count];
        for(int i = 0; i < result.Length; i++)
        {
            result[i] = new Point(vectors[i].x, vectors[i].y);
        }
        return result;
    }

    private void UpdateDraw()
    {
        line.positionCount = pointList.Count;
        line.SetPositions(worldPointList.ToArray());
    }

    private void AddPoint(Vector2 point, Vector3 worldPoint)
    {
        pointList.Add(point);
        worldPointList.Add(worldPoint);
    }

    private void ClearPointList()
    {
        pointList.Clear();
        worldPointList.Clear();
    }

    #endregion
}
