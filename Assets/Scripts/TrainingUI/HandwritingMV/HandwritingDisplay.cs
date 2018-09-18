using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class HandwritingDisplay : MonoBehaviour {

    public UIModel uiModel;
    public HandwritingDisplayType DisplayType;
    LineRenderer line;
    public Vector3 offset;
	void Start () {
        line = GetComponent<LineRenderer>();
	}

	void Update ()
    {
        switch (DisplayType)
        {
            case HandwritingDisplayType.InputLive:
                if (line.positionCount == uiModel.currentInputWorldPoints.Length) return;
                DrawStrokeFromVector3(uiModel.currentInputWorldPoints);
                break;
            case HandwritingDisplayType.InputRaw:
                if (line.positionCount == uiModel.strokeInputPoints.Length) return;
                DrawStrokeFromPoints(uiModel.strokeInputPoints);
                break;
            case HandwritingDisplayType.NegativeRaw:
                if (line.positionCount == uiModel.bestNegativeMatchPoints.Length) return;
                DrawStrokeFromPoints(uiModel.bestNegativeMatchPoints);
                break;
            case HandwritingDisplayType.PositiveRaw:
                if (line.positionCount == uiModel.bestPositiveMatchPoints.Length) return;
                DrawStrokeFromPoints(uiModel.bestPositiveMatchPoints);
                break;
            case HandwritingDisplayType.InputGesture:
                if (line.positionCount == uiModel.strokeInputPoints.Length) return;
                DrawGestureFromPoints(uiModel.strokeInputPoints);
                break;
            case HandwritingDisplayType.PositiveGesture:
                if (line.positionCount == uiModel.strokeInputPoints.Length) return;
                DrawGestureFromPoints(uiModel.bestPositiveMatchPoints);
                break;
            case HandwritingDisplayType.NegativeGesture:
                if (line.positionCount == uiModel.strokeInputPoints.Length) return;
                DrawGestureFromPoints(uiModel.bestNegativeMatchPoints);
                break;
        }

	}

    private void DrawStrokeFromVector3(Vector3[] plot)
    {
        line.positionCount = plot.Length;
        line.SetPositions(plot);
    }

    private void DrawGestureFromPoints(Dollar.One.Point[] points)
    {
        Dollar.One.Unistroke unistroke = new Dollar.One.Unistroke(points, Dollar.One.EvaluationType.ProportionateProtractor);
        DrawStrokeFromPoints(unistroke.Points);
    }

    private void DrawStrokeFromPoints(Dollar.One.Point[] points)
    {
        Vector3[] plot;
        plot = new Vector3[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            plot[i] = Camera.main.ScreenToWorldPoint(new Vector3(points[i].X + offset.x, points[i].Y + offset.y, offset.z));
        }
        line.positionCount = plot.Length;
        line.SetPositions(plot);
    }
}
