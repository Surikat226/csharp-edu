using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;

namespace MatchGame
{
    using System.Windows.Threading;
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthOfSecondsElapsed;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            tenthOfSecondsElapsed++;
            timeTextBlock.Text = (tenthOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
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
                if (textblock.Name != "timeTextBlock")
                {
                    textblock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count); // создаём целочисленный index и присваиваем ему рандомное значение. Берём мы его из текущего кол-ва элементов (Count) списка animalEmoji
                    string nextEmoji = animalEmoji[index]; // создаём строку nextEmoji, которой присваиваем эмодзи. Эмодзи достаём по вышеназначенному индексу
                    textblock.Text = nextEmoji; // берём textblock и обращаемся к его тексту Text, а затем присваиваем ему наш эмодзи
                    animalEmoji.RemoveAt(index); // после всех манипуляций из нашего списка animalEmoji удаляем один эмодзи, таким образом избавляясь от уже использованных эмодзи
                }
            }

            timer.Start();
            tenthOfSecondsElapsed = 0;
            matchesFound = 0;
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
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}