using UnityEngine;
using System.Collections;

public class MouseTouch : GameInput {

    [SerializeField]
    InputGestures gesture = InputGestures.Tap;

    protected override void _Update()
    {
        base._Update();
        MInput();
    }

    void MInput() {
        if (Input.GetMouseButton(0)) {
            GameInputArgs args;
            FillScreenObject(out args, Input.mousePosition);
            args.Gesture = gesture;
            InputToObject(args);
        }
    }















}
