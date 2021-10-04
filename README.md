# Blazor Webassembly SVG Drag And Drop
Blazor Webassembly implementation of drag and drop of SVG objects

[Interactive demo](https://alexeyboiko.github.io/BlazorDraggableDemo/ "Blazor Webassembly SVG Drag And Drop") | [Article](https://alexey-boyko.medium.com/blazor-webassembly-svg-drag-and-drop-e680769ac682)

![Blazor Webassembly SVG Drag And Drop demo](https://github.com/AlexeyBoiko/BlazorDraggableDemo/blob/gh-pages/Blazor-Webassembly-SVG-Drag-And-Drop.gif?raw=true)

## Example of use
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

## How to include Draggable in your project
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
```cs
@inject IMouseService mouseSrv;
 
<g transform="translate(@x, @y)" cursor=@cursor @onmousedown=OnDown 
    @onmousedown:stopPropagation="true">
    @ChildContent
</g>

@code {
    [Parameter] public RenderFragment? ChildContent { get; set; }


    double? x;
    [Parameter]
    public double X { 
        get { return x ?? 0; }
        set { if (!x.HasValue || (!isDown & XChanged.HasDelegate)) { x = value; } } 
    }
    [Parameter] public EventCallback<double> XChanged { get; set; }

    double? y;
    [Parameter]
    public double Y {
        get { return y ?? 0; }
        set { if (!y.HasValue || (!isDown & YChanged.HasDelegate)) { y = value; } }
    }
    [Parameter] public EventCallback<double> YChanged { get; set; }


    protected override void OnInitialized() {
        mouseSrv.OnMove += OnMove;
        mouseSrv.OnUp += OnUp;
        base.OnInitialized();
    }


    string cursor = "grab";
    bool _isDown;
    bool isDown {
        get { return _isDown; }
        set {
            _isDown = value;
            cursor = _isDown ? "grabbing" : "grab";
        }
    }

    double cursorX;
    double cursorY;
    void OnDown(MouseEventArgs e) {
        isDown = true;
        cursorX = e.ClientX;
        cursorY = e.ClientY;
    }

    void OnUp(object? _, MouseEventArgs e) 
        => isDown = false;

    void OnMove(object? _, MouseEventArgs e) {
        if (!isDown)
            return;

        x = x - (cursorX - e.ClientX);
        y = y - (cursorY - e.ClientY);

        cursorX = e.ClientX;
        cursorY = e.ClientY;

        XChanged.InvokeAsync(x.Value);
        YChanged.InvokeAsync(y.Value);
    }

    public void Dispose() {
        mouseSrv.OnMove -= OnMove;
        mouseSrv.OnUp -= OnUp;
    }
}
```

4. Subscribe on SVG events onmousemove and onmouseup, and fire MouseService events
```cs
@inject MouseService mouseSrv;

<svg xmlns="http://www.w3.org/2000/svg"
    @onmousemove=@(e => mouseSrv.FireMove(this, e)) 
    @onmouseup=@(e => mouseSrv.FireUp(this, e))>
    ...
</svg>
```
