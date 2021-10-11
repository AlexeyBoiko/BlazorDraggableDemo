# Blazor Webassembly SVG Drag And Drop
Blazor Webassembly implementation of
- drag and drop of SVG objects
- connecting lines of SVG objects

[Demo](https://alexeyboiko.github.io/BlazorDraggableDemo/ "Blazor Webassembly SVG Drag And Drop") | [Article](https://alexey-boyko.medium.com/blazor-webassembly-svg-drag-and-drop-e680769ac682)

## Drag And Drop

![Blazor Webassembly SVG Drag And Drop demo](https://github.com/AlexeyBoiko/BlazorDraggableDemo/blob/gh-pages/Blazor-Webassembly-SVG-Drag-And-Drop.gif?raw=true)

### Example of use
Without two way binding of X, Y parameters:
```cs
@inject MouseService mouseSrv;

<svg xmlns="http://www.w3.org/2000/svg"
    @onmousemove=@(e => mouseSrv.FireMove(this, e)) 
    @onmouseup=@(e => mouseSrv.FireUp(this, e))>

    <Draggable X=250 Y=150>
        <circle r="60" fill="#ff6600" />
        <text text-anchor="middle" alignment-baseline="central" style="fill:#fff;">Sun</text>
    </Draggable>
</svg>
```

With two way binding:
```cs
@inject MouseService mouseSrv;

<svg xmlns="http://www.w3.org/2000/svg"
    @onmousemove=@(e => mouseSrv.FireMove(this, e)) 
    @onmouseup=@(e => mouseSrv.FireUp(this, e))>>

    <Draggable @bind-X=X @bind-Y=Y>
       <circle r="60" fill="#ff6600" />
        <text text-anchor="middle" alignment-baseline="central" style="fill:#fff;">Sun</text>
    </Draggable>
</svg>

@code {
    double X = 250;
    double Y = 150;
}
```

### How to include Draggable in your project
1. Create MouseService
```cs
// inject IMouseService into subscribers
public interface IMouseService {
    event EventHandler<MouseEventArgs>? OnMove;
    event EventHandler<MouseEventArgs>? OnUp;
}

// use MouseService to fire events
public class MouseService : IMouseService {
    public event EventHandler<MouseEventArgs>? OnMove;
    public event EventHandler<MouseEventArgs>? OnUp;

    public void FireMove(object obj, MouseEventArgs evt) => OnMove?.Invoke(obj, evt);
    public void FireUp(object obj, MouseEventArgs evt) => OnUp?.Invoke(obj, evt);
}
```

2. Register MouseService in Program.cs
```cs
builder.Services
	.AddSingleton<MouseService>()
	.AddSingleton<IMouseService>(ff => ff.GetRequiredService<MouseService>());
```

3. Create Draggable component
Copy and paste Draggable component code from source code.

4. Subscribe on SVG events onmousemove and onmouseup, and fire MouseService events
```cs
@inject MouseService mouseSrv;

<svg xmlns="http://www.w3.org/2000/svg"
    @onmousemove=@(e => mouseSrv.FireMove(this, e)) 
    @onmouseup=@(e => mouseSrv.FireUp(this, e))>
    ...
</svg>
```
## Connecting Lines
![Blazor Webassembly SVG Connectors demo](https://raw.githubusercontent.com/AlexeyBoiko/BlazorDraggableDemo/gh-pages/Blazor-Webassembly-SVG-Connectors.gif)

### Example of use

```cs
<svg xmlns="http://www.w3.org/2000/svg">

    <Connector 
        X1=100 Y1=100 
        Dir1=Direction.Right
                
        X2=300 Y2=250
        Dir2=Direction.Left />

</svg>

```

### How to include Connector in your project
Just copy and past Connector component from source code.
