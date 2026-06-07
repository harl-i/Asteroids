namespace Game.Presentation.Common
{
    public interface IBindableView<TModel>
    {
       public void Bind(TModel model);
    }
}
