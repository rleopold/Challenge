using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Challenge.Domain;

namespace Challenge.Repositories
{
    //for the sake of this excercise, consider this class a total blackbox
    public class WidgetRepo : IWidgetRepo
    {
        private readonly IList<Widget> _widgets = new List<Widget>();
        private int _connectionCount = 0;
        public async Task CommitWidget(Widget widget)
        {
            ++_connectionCount;
            if(_connectionCount > 20)
                throw new System.Exception("You have overloaded the widget repo.");
            await Task.Delay(new Random().Next(100,200));
            _widgets.Add(widget);
            --_connectionCount;
        }
    }
}