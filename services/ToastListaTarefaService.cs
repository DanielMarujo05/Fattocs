using BlazorStrap;

namespace Fattocs.services
{
    public interface IToastListaTarefaService
    {
        void Show(string titulomensagem, string message, BSColor Color);
    }
    public class ToastListaTarefaService : IToastListaTarefaService
    {
        private readonly IBlazorStrap _blazorStrap;

        public ToastListaTarefaService(IBlazorStrap toastService)
        {
            _blazorStrap = toastService;
        }

        public void Show(string titulomensagem, string message, BSColor color)
        {
            _blazorStrap.Toaster.Add(titulomensagem, message, o =>
            {
                o.Color = color;
                o.CloseAfter = 5000;
                o.Toast = Toast.TopRight;
                o.HasIcon = true;
            });
        }
    }
}
