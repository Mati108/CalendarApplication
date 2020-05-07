using CalendarApplication.UI.ViewModel;
using NUnit.Framework;

namespace Tests
{
    public class ViewModelBaseTests
    {
        [Test]
        public void PropertyChanged_ComparingWithEmptyWord_ViewThatNothingChanges()
        {
            ViewModelBase viewModelBase = new ViewModelBase();
            string changedPropertyName = string.Empty;
            viewModelBase.PropertyChanged += (sender, args) => changedPropertyName = args.PropertyName;
            Assert.AreEqual("", changedPropertyName);
        }
    }
}
