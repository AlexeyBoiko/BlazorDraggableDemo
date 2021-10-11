using Microsoft.AspNetCore.Components.Web;

namespace BlazorDraggableDemo.Services {
    public interface IMouseService {
        event EventHandler<MouseEventArgs>? OnMove;
        event EventHandler<MouseEventArgs>? OnUp;
        event EventHandler<MouseEventArgs>? OnLeave;
    }

    public class MouseService : IMouseService {
        public event EventHandler<MouseEventArgs>? OnMove;
        public event EventHandler<MouseEventArgs>? OnUp;
        public event EventHandler<MouseEventArgs>? OnLeave;

        public void FireMove(object obj, MouseEventArgs evt) => OnMove?.Invoke(obj, evt);
        public void FireUp(object obj, MouseEventArgs evt) => OnUp?.Invoke(obj, evt);
        public void FireLeave(object obj, MouseEventArgs evt) => OnLeave?.Invoke(obj, evt);
    }
}
