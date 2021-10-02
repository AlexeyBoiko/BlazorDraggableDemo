using Microsoft.AspNetCore.Components.Web;
using System;

namespace BlazorDraggableDemo.Services {

    public interface IMouseService {
        event EventHandler<MouseEventArgs>? OnMove;
        event EventHandler<MouseEventArgs>? OnUp;
    }

    public class MouseService : IMouseService {
        public event EventHandler<MouseEventArgs>? OnMove;
        public event EventHandler<MouseEventArgs>? OnUp;

        public void FireMove(object obj, MouseEventArgs evt) => OnMove?.Invoke(obj, evt);
        public void FireUp(object obj, MouseEventArgs evt) => OnUp?.Invoke(obj, evt);
    }
}
