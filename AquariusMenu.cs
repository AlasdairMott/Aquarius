using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Rhino;
using Rhino.Commands;
using Rhino.UI;

using Rhino.PlugIns;
using RhinoWindows.Controls;

using Grasshopper.Kernel;
using Grasshopper;

namespace Aquarius
{
	class AquariusMenu
    {
        Eto.Forms.UITimer _timer;

        //private WinFormsDockBar _viewportControlPanel;

        //RhinoWindows.Controls.DockBar.Show(WinFormsDockBar.BarId, false);

        //private AquariusDialog _viewportControlPanel
        //{
        //    get;
        //    set;
        //}

        //private void OnFormClosed(object sender, EventArgs e)
        //{
        //    _viewportControlPanel.SavePosition();
        //    _viewportControlPanel.Dispose();
        //    _viewportControlPanel = null;
        //}

        //private 
        static WinFormsDockBar _canvasViewport;

        private void CreateMyDockBar()
        {
            var create_options = new DockBarCreateOptions
            {
                DockLocation = DockBarDockLocation.Floating,
                Visible = false,
                DockStyle = DockBarDockStyle.Any,
                FloatPoint = new System.Drawing.Point(200, 20)
            };

            _canvasViewport = new WinFormsDockBar();
            _canvasViewport.Create(create_options);
        }

        public void AddToMenu()
        {
            if (_timer != null)
                return;
            _timer = new Eto.Forms.UITimer();
            _timer.Interval = 1;
            _timer.Elapsed += SetupMenu;
            _timer.Start();
        }

        void SetupMenu(object sender, EventArgs e)
        {
            var editor = Grasshopper.Instances.DocumentEditor;
            if (null == editor || editor.Handle == IntPtr.Zero)
                return;

            var controls = editor.Controls;
            if (null == controls || controls.Count == 0)
                return;

            _timer.Stop();
            foreach (var ctrl in controls)
            {
                var menu = ctrl as Grasshopper.GUI.GH_MenuStrip;
                if (menu == null)
                    continue;
                for (int i = 0; i < menu.Items.Count; i++)
                {
                    var menuitem = menu.Items[i] as ToolStripMenuItem;
                    if (menuitem != null && menuitem.Text == "Display")
                    {
                        for (int j = 0; j < menuitem.DropDownItems.Count; j++)
                        {
                            if (menuitem.DropDownItems[j].Text.StartsWith("canvas widgets", StringComparison.OrdinalIgnoreCase))
                            {
                                var viewportMenuItem = new ToolStripMenuItem("Canvas Controls");
                                viewportMenuItem.CheckOnClick = true;
                                menuitem.DropDownOpened += (s, args) =>
                                {
                                    if (_canvasViewport!= null && _canvasViewport.IsVisible)
                                    {
                                        viewportMenuItem.Checked = true;
                                    }
                                    else
                                    {
                                        viewportMenuItem.Checked = false;
                                    }
                                    //if (_viewportcontrolpanel != null && _viewportcontrolpanel.visible)
                                    //    viewportmenuitem.checked = true;
                                    //else
                                    //    viewportmenuitem.checked = false;
                                };

                                viewportMenuItem.CheckedChanged += ViewportMenuItem_CheckedChanged;

                                var canvasWidgets = menuitem.DropDownItems[j] as ToolStripMenuItem;
                                if (canvasWidgets != null)
                                {
                                    canvasWidgets.DropDownOpening += (s, args) =>
                                        canvasWidgets.DropDownItems.Insert(0, viewportMenuItem);
                                }
                                break;
                            }
                        }
                        break;
                    }
                }
            }
        }

        void ViewportMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            var menuitem = sender as ToolStripMenuItem;
            if (menuitem != null)
            {
                if (menuitem.Checked)
                {
                    if (_canvasViewport == null) 
                    {
                        CreateMyDockBar();
                    }

                    RhinoWindows.Controls.DockBar.Show(WinFormsDockBar.BarId, false);
                    //if (_viewportControlPanel == null)
                    //{
                    //    _viewportControlPanel = new AquariusDialog { Owner = RhinoEtoApp.MainWindow };



                    //    //_viewportControlPanel = new AquariusModalDialog();
                    //    //_viewportControlPanel.RestorePosition();

                    //    //var dialog_rc = _viewportControlPanel.ShowModal(RhinoEtoApp.MainWindow);

                    //    //_viewportControlPanel = new ViewportContainerPanel();
                    //    //_viewportControlPanel.Size = new System.Drawing.Size(400, 300);
                    //    //_viewportControlPanel.MinimumSize = new System.Drawing.Size(50, 50);
                    //    //_viewportControlPanel.Padding = new Padding(10);
                    //    //var ctrl = new CanvasViewportControl();
                    //    //ctrl.Dock = DockStyle.Fill;

                    //    //ctrl.ContextMenu = contextMenu;
                    //    //_viewportControlPanel.BorderStyle = BorderStyle.FixedSingle;
                    //    //_viewportControlPanel.Controls.Add(ctrl);
                    //    //_viewportControlPanel.Location = new System.Drawing.Point(0, 0);
                    //    //_viewportControlPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    //    //Grasshopper.Instances.ActiveCanvas.Controls.Add(_viewportControlPanel);
                    //    //Dock(AnchorStyles.Top | AnchorStyles.Right);
                    //}
                    ////_viewportControlPanel.Show();
                    //_viewportControlPanel.RestorePosition();
                    //_viewportControlPanel.Closed += OnFormClosed;
                    //_viewportControlPanel.Show();

                }
                else
                {
                    if (_canvasViewport != null && _canvasViewport.IsVisible)
                    {
                        RhinoWindows.Controls.DockBar.Hide(WinFormsDockBar.BarId, false);
                    }
                    
                    //if (_viewportControlPanel != null && _viewportControlPanel.Visible)
                    //    _viewportControlPanel.Close();

                }
            }
        }
    }

    /// <summary>
	/// WinFormsDockBar dockbar class
	/// </summary>
	internal class WinFormsDockBar : DockBar
    {
        public static Guid BarId => new Guid("{56366e9e-9a32-44c7-a2a6-d701a4e13b07}");
        public WinFormsDockBar() : base(PlugIn.Find(Instances.GrasshopperPluginId), BarId, "Aquarius Controls")
        {
            SetContentControl(new Forms.UserControl1());
        }
    }
}
