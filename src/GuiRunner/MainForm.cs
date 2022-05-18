using Notadesigner.Tommy.App.Properties;
using Notadesigner.Tommy.Core;

namespace Notadesigner.Tommy.App
{
    public partial class MainForm : Form
    {
        private readonly PomoEngine _engine;

        private readonly CircularProgressBar.CircularProgressBar[] _allProgressBars = new CircularProgressBar.CircularProgressBar[]
        {
            ProgressBarFactory.Create(),
            ProgressBarFactory.Create(),
            ProgressBarFactory.Create(),
            ProgressBarFactory.Create()
        };

        private CircularProgressBar.CircularProgressBar _activeProgressBar;

        public MainForm(PomoEngine engine)
        {
            _ = engine ?? throw new ArgumentNullException(nameof(engine));

            _engine = engine;
            _engine.Progress += EngineProgressHandler;
            _engine.StateChange += EngineStateChangeHandler;
            _engine.StartSession += EngineStartSessionHandler;

            InitializeComponent();

            Icon = GuiRunnerResources.MainIcon;

            Array.ForEach(_allProgressBars, p => ProgressBarsContainer.Controls.Add(p));
            _activeProgressBar = _allProgressBars[0];
        }

        private void EngineStartSessionHandler(object? sender, EventArgs e)
        {
            Array.ForEach(_allProgressBars, p =>
            {
                p.ProgressColor = SystemColors.Highlight;
                p.Text = "__:__ / __:__";
                p.Value = 0;
            });
        }

        private void StartBreakClickHandler(object sender, EventArgs e)
        {
            _engine.MoveNext();
        }

        private void EngineProgressHandler(object? sender, ProgressEventArgs e)
        {
            _activeProgressBar.Invoke(() =>
            {
                _activeProgressBar.Value = Convert.ToInt32(e.ElapsedDuration.TotalSeconds);
                _activeProgressBar.Maximum = Convert.ToInt32(e.TotalDuration.TotalSeconds);
                _activeProgressBar.Text = $"{e.ElapsedDuration:mm\\:ss} / {e.TotalDuration:mm\\:ss}";
            });
        }

        private void EngineStateChangeHandler(object? sender, StateChangeEventArgs e)
        {
            if (e.State == EngineState.AppReady)
            {
                _activeProgressBar = _allProgressBars[e.RoundCounter];
            }

            if (e.State == EngineState.WorkCompleted)
            {
                if (GuiRunnerSettings.Default.AutoAdvance)
                {
                    _engine.MoveNext();
                }
                else
                {
                }

                _activeProgressBar.ProgressColor = SystemColors.GradientActiveCaption;
            }

            if (e.State == EngineState.BreakCompleted)
            {
                if (GuiRunnerSettings.Default.AutoAdvance)
                {
                    _engine.MoveNext();
                }
                else
                {
                }

                _activeProgressBar = _allProgressBars[e.RoundCounter];
                _activeProgressBar.ProgressColor = SystemColors.Highlight;
            }
        }
    }
}