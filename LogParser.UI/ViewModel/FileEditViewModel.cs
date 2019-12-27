using LogParser.Model;

namespace LogParser.UI.ViewModel
{
    public interface IFileDataEditViewModel
    {
        void Load(int eventDataId);
        EventData eventData { get; }
    }
    public class FileDataEditViewModel : ViewModelBase, IFileDataEditViewModel
    {
        public EventData eventData => throw new System.NotImplementedException();

        public void Load(int eventDataId)
        {

        }
    }
}
