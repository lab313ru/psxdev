﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DerouteSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            entityBox1.AssociateSelectionPropertyGrid(propertyGrid2);

            entityBox1.Mode = EntityMode.Selection;

            SelectionButtonHighlight();

            entityBox1.OnScrollChanged += ScrollChanged;
            entityBox1.OnZoomChanged += ZoomChanged;
            entityBox1.OnEntityCountChanged += EntityCountChanged;
            entityBox1.OnLastOperation += LastOperation;
            entityBox1.OnEntityLabelEdit += EntityLabelChanged;

            entityBox1.BeaconImage = Properties.Resources.beacon_entity;
        }

        private void ScrollChanged(object sender, EventArgs e)
        {
            EntityBox entityBox = (EntityBox)sender;

            toolStripStatusLabel2.Text = entityBox.ScrollX.ToString() + "; " +
                                         entityBox.ScrollY.ToString();
        }

        private void ZoomChanged(object sender, EventArgs e)
        {
            EntityBox entityBox = (EntityBox)sender;

            toolStripStatusLabel4.Text = entityBox.Zoom.ToString() + "%";
        }

        private void EntityCountChanged(object sender, EventArgs e)
        {
            EntityBox entityBox = (EntityBox)sender;

            toolStripStatusLabel6.Text = entityBox.GetViasCount().ToString();
            toolStripStatusLabel8.Text = entityBox.GetWireCount().ToString();
            toolStripStatusLabel10.Text = entityBox.GetCellCount().ToString();

            //
            // Update beacon list
            //

            if ( listView1.Items.Count != entityBox1.GetBeaconCount() )
            {
                RebuildBeaconList();
            }
        }

        private void LastOperation(object sender, EventArgs e)
        {
            EntityBox entityBox = (EntityBox)sender;

            toolStripStatusLabel12.Text = entityBox.GetLastOperation().ToString();
        }

        private void EntityLabelChanged(object sender, Entity entity, EventArgs e)
        {
            if (entity.Type == EntityType.Beacon)
            {
                RebuildBeaconList();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About aboutDialog = new About();
            aboutDialog.ShowDialog();
        }

        private void deleteAllEntitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.DeleteAllEntites();
        }

        private void loadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();

            if ( result == DialogResult.OK )
            {
                Image image = Image.FromFile(openFileDialog1.FileName);
                entityBox1.LoadImage(image);
            }
        }

        private void saveSceneAsImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                entityBox1.SaveSceneAsImage(saveFileDialog1.FileName);
            }
        }

        private void loadEntitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog2.ShowDialog();

            if (result == DialogResult.OK)
            {
                entityBox1.Unserialize(openFileDialog2.FileName, true);
            }
        }

        private void saveEntitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog2.ShowDialog();

            if (result == DialogResult.OK)
            {
                entityBox1.Serialize(saveFileDialog2.FileName);
            }
        }

        private void SelectionButtonHighlight ()
        {
            toolStripDropDownButton4.BackColor = SystemColors.ActiveCaption;
            toolStripDropDownButton1.BackColor = SystemColors.Control;
            toolStripDropDownButton2.BackColor = SystemColors.Control;
            toolStripDropDownButton3.BackColor = SystemColors.Control;
            toolStripButton5.BackColor = SystemColors.Control;
        }

        private void ViasButtonHighlight()
        {
            toolStripDropDownButton4.BackColor = SystemColors.Control;
            toolStripDropDownButton1.BackColor = SystemColors.ActiveCaption;
            toolStripDropDownButton2.BackColor = SystemColors.Control;
            toolStripDropDownButton3.BackColor = SystemColors.Control;
            toolStripButton5.BackColor = SystemColors.Control;
        }

        private void WiresButtonHighlight()
        {
            toolStripDropDownButton4.BackColor = SystemColors.Control;
            toolStripDropDownButton1.BackColor = SystemColors.Control;
            toolStripDropDownButton2.BackColor = SystemColors.ActiveCaption;
            toolStripDropDownButton3.BackColor = SystemColors.Control;
            toolStripButton5.BackColor = SystemColors.Control;
        }

        private void CellsButtonHighlight()
        {
            toolStripDropDownButton4.BackColor = SystemColors.Control;
            toolStripDropDownButton1.BackColor = SystemColors.Control;
            toolStripDropDownButton2.BackColor = SystemColors.Control;
            toolStripDropDownButton3.BackColor = SystemColors.ActiveCaption;
            toolStripButton5.BackColor = SystemColors.Control;
        }

        private void BeaconButtonHighlight ()
        {
            toolStripDropDownButton4.BackColor = SystemColors.Control;
            toolStripDropDownButton1.BackColor = SystemColors.Control;
            toolStripDropDownButton2.BackColor = SystemColors.Control;
            toolStripDropDownButton3.BackColor = SystemColors.Control;
            toolStripButton5.BackColor = SystemColors.ActiveCaption;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            entityBox1.MergeSelectedWires(false);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            entityBox1.MergeSelectedWires(true);
        }

        private void wireInterconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.WireInterconnect;
            propertyGrid1.Refresh();
            WiresButtonHighlight();
        }

        private void wirePowerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.WirePower;
            propertyGrid1.Refresh();
            WiresButtonHighlight();
        }

        private void wireGroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.WireGround;
            propertyGrid1.Refresh();
            WiresButtonHighlight();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.ViasConnect;
            propertyGrid1.Refresh();
            ViasButtonHighlight();
        }

        private void viasPowerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.ViasPower;
            propertyGrid1.Refresh();
            ViasButtonHighlight();
        }

        private void viasGroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.ViasGround;
            propertyGrid1.Refresh();
            ViasButtonHighlight();
        }

        private void viasInputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.ViasInput;
            propertyGrid1.Refresh();
            ViasButtonHighlight();
        }

        private void viasOutputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.ViasOutput;
            propertyGrid1.Refresh();
            ViasButtonHighlight();
        }

        private void viasInoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.ViasInout;
            propertyGrid1.Refresh();
            ViasButtonHighlight();
        }

        private void viasFloatingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.ViasFloating;
            propertyGrid1.Refresh();
            ViasButtonHighlight();
        }

        private void cellNotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.CellNot;
            propertyGrid1.Refresh();
            CellsButtonHighlight();
        }

        private void cellBufferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.CellBuffer;
            propertyGrid1.Refresh();
            CellsButtonHighlight();
        }

        private void cellMuxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.CellMux;
            propertyGrid1.Refresh();
            CellsButtonHighlight();
        }

        private void cellLogicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.CellLogic;
            propertyGrid1.Refresh();
            CellsButtonHighlight();
        }

        private void cellAdderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.CellAdder;
            propertyGrid1.Refresh();
            CellsButtonHighlight();
        }

        private void cellBusSupportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.CellBusSupp;
            propertyGrid1.Refresh();
            CellsButtonHighlight();
        }

        private void cellFlipFlopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.CellFlipFlop;
            propertyGrid1.Refresh();
            CellsButtonHighlight();
        }

        private void cellLatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.CellLatch;
            propertyGrid1.Refresh();
            CellsButtonHighlight();
        }

        private void cellOtherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.CellOther;
            propertyGrid1.Refresh();
            CellsButtonHighlight();
        }

        private void unitRegisterFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.UnitRegfile;
            propertyGrid1.Refresh();
            CellsButtonHighlight();
        }

        private void unitMemoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.UnitMemory;
            propertyGrid1.Refresh();
            CellsButtonHighlight();
        }

        private void unitCustomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.UnitCustom;
            propertyGrid1.Refresh();
            CellsButtonHighlight();
        }

        private void sceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.Selection;
            propertyGrid1.Refresh();
            SelectionButtonHighlight();
        }

        private void image0ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.ImageLayer0;
            propertyGrid1.Refresh();
            SelectionButtonHighlight();
        }

        private void image1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.ImageLayer1;
            propertyGrid1.Refresh();
            SelectionButtonHighlight();
        }

        private void image2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.ImageLayer2;
            propertyGrid1.Refresh();
            SelectionButtonHighlight();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            entityBox1.DeleteSelected();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.Selection;
            propertyGrid1.Refresh();
            SelectionButtonHighlight();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.ViasConnect;
            propertyGrid1.Refresh();
            ViasButtonHighlight();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.WireInterconnect;
            propertyGrid1.Refresh();
            WiresButtonHighlight();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 )
            {
                entityBox1.Mode = EntityMode.Selection;
                propertyGrid1.Refresh();
                SelectionButtonHighlight();
            }
            else if (e.KeyCode == Keys.F2 )
            {
                entityBox1.Mode = EntityMode.ViasConnect;
                propertyGrid1.Refresh();
                ViasButtonHighlight();
            }
            else if (e.KeyCode == Keys.F3 )
            {
                entityBox1.Mode = EntityMode.WireInterconnect;
                propertyGrid1.Refresh();
                WiresButtonHighlight();
            }
            else if ( e.KeyCode == Keys.Z && e.Control )
            {
                entityBox1.CancelLastOperation();
            }
            else if (e.KeyCode == Keys.Y && e.Control)
            {
                entityBox1.RetryCancelledOperation();
            }
            else if ( e.KeyCode == Keys.F10 )
            {
                entityBox1.TraversalSelection(1);
            }
            else if (e.KeyCode == Keys.F11)
            {
                entityBox1.TraversalSelection(2);
            }
            else if (e.KeyCode == Keys.F12)
            {
                entityBox1.TraversalSelection(3);
            }
        }

        private void SetLayerOpacity (int opacity)
        {
            switch (entityBox1.Mode)
            {
                case EntityMode.ImageLayer0:
                default:
                    entityBox1.ImageOpacity0 = opacity;
                    entityBox1.Invalidate();
                    break;
                case EntityMode.ImageLayer1:
                    entityBox1.ImageOpacity1 = opacity;
                    entityBox1.Invalidate();
                    break;
                case EntityMode.ImageLayer2:
                    entityBox1.ImageOpacity2 = opacity;
                    entityBox1.Invalidate();
                    break;
            }
        }

        private void SetLayerOrigin()
        {
            PointF zero = new PointF(0, 0);

            switch (entityBox1.Mode)
            {
                case EntityMode.ImageLayer0:
                default:
                    entityBox1.ScrollImage0 = zero;
                    entityBox1.Invalidate();
                    break;
                case EntityMode.ImageLayer1:
                    entityBox1.ScrollImage1 = zero;
                    entityBox1.Invalidate();
                    break;
                case EntityMode.ImageLayer2:
                    entityBox1.ScrollImage2 = zero;
                    entityBox1.Invalidate();
                    break;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SetLayerOpacity(50);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SetLayerOpacity(75);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SetLayerOpacity(100);
        }

        private void setLayerScrollToOriginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLayerOrigin();
        }

        private void loadWorkspaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog2.ShowDialog();

            if (result == DialogResult.OK)
            {
                entityBox1.LoadWorkspace(openFileDialog2.FileName);
            }
        }

        private void saveWorkspaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog2.ShowDialog();

            if (result == DialogResult.OK)
            {
                entityBox1.SaveWorkspace(saveFileDialog2.FileName);
            }
        }

        private void cancelOperationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.CancelLastOperation();
        }

        private void repeatCancelledOperationCtrlYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.RetryCancelledOperation();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            entityBox1.DrawWireBetweenSelectedViases();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            entityBox1.Mode = EntityMode.Beacon;
            propertyGrid1.Refresh();
            BeaconButtonHighlight();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            ListView listView = (ListView)sender;

            if (listView.SelectedItems.Count > 0)
            {
                ListViewItem selected = listView.SelectedItems[0];
                Entity beacon = (Entity)selected.Tag;
                entityBox1.ScrollToBeacon(beacon);

                //
                // Switch to selection mode
                //

                entityBox1.Mode = EntityMode.Selection;
                propertyGrid1.Refresh();
                SelectionButtonHighlight();
            }
        }

        private void RebuildBeaconList ()
        {
            listView1.Items.Clear();
            List<Entity> beacons = entityBox1.GetBeacons();

            int id = 0;

            foreach (Entity beacon in beacons)
            {
                ListViewItem item = new ListViewItem(id.ToString());
                item.Tag = beacon;
                item.SubItems.Add(beacon.Label);
                listView1.Items.Add(item);

                id++;
            }
        }

        private void traverseTIER1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.TraversalSelection(1);
        }

        private void traverseTIER2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.TraversalSelection(2);
        }

        private void traverseTIER3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.TraversalSelection(3);
        }

        private void traverseTIER5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            entityBox1.TraversalSelection(5);
        }

        private void keyBindingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KeyBind keyBindDialog = new KeyBind();
            keyBindDialog.Show();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            entityBox1.WireRecognize();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            entityBox1.WireExtendHead();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            entityBox1.WireExtendTail();
        }
    }       // Form1
}
