﻿using ACT.Radardata;
using ACT.RadarForm;
using ACT.RadarViewOrder;
using Advanced_Combat_Tracker;
using MultiProject;
using MultiProject.Common;
using System.Timers;
namespace MultiRadar
{
    public partial class RadarSettingControl
    {
        protected RadarForm radarForm;
        protected AlertForm alertForm;
        Analyze analyze = new Analyze();

        partial void oFormActMain_OnCombatStart(bool isImport, CombatToggleEventArgs encount)
        {
           
            BasePlugin.BattleTimerStart();
            BasePlugin.combatOn = true;
        }
        partial void oFormActMain_OnCombatEnd(bool isImport, CombatToggleEventArgs encount)
        {
            BasePlugin.combatOn = false;
            BasePlugin.BattleTimerEnd();
        }
        partial void oFormActMain_AfterCombatAction(bool isImport, CombatActionEventArgs actionInfo)
        {
        }

        partial void oFormActMain_BeforeLogLineRead(bool isImport, LogLineEventArgs actionInfo)
        {
            if (analyze.AnalyzeLogLine(actionInfo.logLine))
            {
                switch (analyze.chenge)
                {
                    case AnalyzeBase.ChengeParameter.changedZone:
                        break;
                }
            }
        }

        partial void oClock_Action(object sender, ElapsedEventArgs e)
        {
            ActData.AllCharactor = ActHelper.GetCombatantList();
            if (ActData.AllCharactor.Count > 0)
            {
                radarForm.upDate();
                //CallSetLine();
            }
        }
        partial void ViewWindow()
        {
            if (ckRadarVisible.Checked)
            {

                radarForm = new RadarForm();
                radarForm.Left = int.Parse(textRadarXpos.Text);
                radarForm.Top = int.Parse(textRadarYpos.Text);
                radarForm.CallbackSaveSetting = SaveSettings;

                RadarViewOrder.SoundEnable = ckRadarSE.Checked;
                alertForm = new AlertForm();
                alertForm.CallbackSaveSetting = SaveSettings;
                alertForm.Left = int.Parse(textAlertXpos.Text);
                alertForm.Top = int.Parse(textAlertYpos.Text);

                alertForm.Show();

                RadardataInstance.SetRadarData(textRadarDataPath.Text + "RadarData.xml");
                ReSetComboRadarZoneItem(false);
                RadarViewOrder.AllRadarMode = rbRederModeFull.Checked;
                radarForm.Show();
            }
        }
        partial void CloseWindow()
        {
            if (radarForm != null)
            {
                radarForm.Hide();
                radarForm.Dispose();
            }
            if (alertForm != null)
            {
                alertForm.Hide();
                alertForm.Dispose();
            }
        }
    }
}
