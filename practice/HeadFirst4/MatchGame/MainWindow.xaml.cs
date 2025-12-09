using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MatchGame
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetUpGame();
        }

        private void SetUpGame() // объявляем приватный(?) метод SetUpGame
        {
            // Объявляем список List, который будет состоять из стрингов и присваиваем ему имя animalEmoji
            // =
            // создаём новый список, который будет состоять из стрингов - так же указываем это в <string>
            List<string> animalEmoji = new List<string>()
            // Наполняем список нашими стрингами - эмодзи
            {
                "🦘", "🦘",
                "🐢", "🐢",
                "🦎", "🦎",
                "🐬", "🐬",
                "🐷", "🐷",
                "🦧", "🦧",
                "🦆", "🦆",
                "🦊", "🦊"
            };
            Random random = new Random(); // создаём объект random класса Random
            // Проходимся по каждому элементу textblock класса TextBlock, лежащими внутри XAML, а именно - по детям TextBlock внутри сетки с именем mainGrid
            foreach (TextBlock textblock in mainGrid.Children.OfType<TextBlock>())
            {
                int index = random.Next(animalEmoji.Count); // создаём целочисленный index и присваиваем ему рандомное значение. Берём мы его из текущего кол-ва элементов (Count) списка animalEmoji
                string nextEmoji = animalEmoji[index]; // создаём строку nextEmoji, которой присваиваем эмодзи. Эмодзи достаём по вышеназначенному индексу
                textblock.Text = nextEmoji; // берём textblock и обращаемся к его тексту Text, а затем присваиваем ему наш эмодзи
                animalEmoji.RemoveAt(index); // после всех манипуляций из нашего списка animalEmoji удаляем один эмодзи, таким образом избавляясь от уже использованных эмодзи
            }
        }

        TextBlock lastTextBlockClicked;
        bool findingMatch = false;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock; if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlockClicked.Text)
            {
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }
    }
}