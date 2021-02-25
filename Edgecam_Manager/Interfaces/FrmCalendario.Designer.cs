namespace Edgecam_Manager
{
    partial class FrmCalendario
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCalendario));
            this.ultraCalendarInfo1 = new Infragistics.Win.UltraWinSchedule.UltraCalendarInfo(this.components);
            this.ultraCalendarLook1 = new Infragistics.Win.UltraWinSchedule.UltraCalendarLook(this.components);
            this.ultraTilePanel1 = new Infragistics.Win.Misc.UltraTilePanel();
            this.ultraTile1 = new Infragistics.Win.Misc.UltraTile();
            this.ultraTile2 = new Infragistics.Win.Misc.UltraTile();
            this.ultraTile3 = new Infragistics.Win.Misc.UltraTile();
            this.ultraDayView1 = new Infragistics.Win.UltraWinSchedule.UltraDayView();
            this.ultraMonthViewSingle1 = new Infragistics.Win.UltraWinSchedule.UltraMonthViewSingle();
            this.ultraWeekView1 = new Infragistics.Win.UltraWinSchedule.UltraWeekView();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTilePanel1)).BeginInit();
            this.ultraTilePanel1.SuspendLayout();
            this.ultraTile1.SuspendLayout();
            this.ultraTile2.SuspendLayout();
            this.ultraTile3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDayView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraMonthViewSingle1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraWeekView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraCalendarInfo1
            // 
            this.ultraCalendarInfo1.DataBindingsForAppointments.BindingContextControl = this;
            this.ultraCalendarInfo1.DataBindingsForOwners.BindingContextControl = this;
            this.ultraCalendarInfo1.FirstDayOfWeek = Infragistics.Win.UltraWinSchedule.FirstDayOfWeek.Monday;
            this.ultraCalendarInfo1.SaveSettingsCategories = ((Infragistics.Win.UltraWinSchedule.CalendarInfoCategories)(((((((((Infragistics.Win.UltraWinSchedule.CalendarInfoCategories.General | Infragistics.Win.UltraWinSchedule.CalendarInfoCategories.AppearancesCollection) 
            | Infragistics.Win.UltraWinSchedule.CalendarInfoCategories.Holidays) 
            | Infragistics.Win.UltraWinSchedule.CalendarInfoCategories.Notes) 
            | Infragistics.Win.UltraWinSchedule.CalendarInfoCategories.DaysOfWeek) 
            | Infragistics.Win.UltraWinSchedule.CalendarInfoCategories.DaysOfMonth) 
            | Infragistics.Win.UltraWinSchedule.CalendarInfoCategories.DaysOfYear) 
            | Infragistics.Win.UltraWinSchedule.CalendarInfoCategories.MonthsOfYear) 
            | Infragistics.Win.UltraWinSchedule.CalendarInfoCategories.WeeksOfYear)));
            this.ultraCalendarInfo1.AfterActiveDayChanged += new Infragistics.Win.UltraWinSchedule.AfterActiveDayChangedEventHandler(this.ultraCalendarInfo1_AfterActiveDayChanged);
            // 
            // ultraCalendarLook1
            // 
            this.ultraCalendarLook1.ViewStyle = Infragistics.Win.UltraWinSchedule.ViewStyle.Office2007;
            // 
            // ultraTilePanel1
            // 
            this.ultraTilePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraTilePanel1.Location = new System.Drawing.Point(0, 0);
            this.ultraTilePanel1.Name = "ultraTilePanel1";
            this.ultraTilePanel1.NormalModeDimensions = new System.Drawing.Size(2, 2);
            this.ultraTilePanel1.Size = new System.Drawing.Size(725, 532);
            this.ultraTilePanel1.TabIndex = 0;
            this.ultraTilePanel1.Tiles.Add(this.ultraTile1);
            this.ultraTilePanel1.Tiles.Add(this.ultraTile2);
            this.ultraTilePanel1.Tiles.Add(this.ultraTile3);
            // 
            // ultraTile1
            // 
            this.ultraTile1.Caption = "Dia";
            this.ultraTile1.Control = this.ultraDayView1;
            this.ultraTile1.Controls.Add(this.ultraDayView1);
            this.ultraTile1.IndexInLargeTileCollection = 0;
            this.ultraTile1.Name = "ultraTile1";
            this.ultraTile1.PositionInNormalMode = new System.Drawing.Point(0, 0);
            this.ultraTile1.State = Infragistics.Win.Misc.TileState.Large;
            this.ultraTile1.TabIndex = 0;
            // 
            // ultraTile2
            // 
            this.ultraTile2.Caption = "Semana";
            this.ultraTile2.Control = this.ultraWeekView1;
            this.ultraTile2.Controls.Add(this.ultraWeekView1);
            this.ultraTile2.Name = "ultraTile2";
            this.ultraTile2.PositionInNormalMode = new System.Drawing.Point(1, 0);
            this.ultraTile2.TabIndex = 1;
            // 
            // ultraTile3
            // 
            this.ultraTile3.Caption = "Mês";
            this.ultraTile3.Control = this.ultraMonthViewSingle1;
            this.ultraTile3.Controls.Add(this.ultraMonthViewSingle1);
            this.ultraTile3.Name = "ultraTile3";
            this.ultraTile3.PositionInNormalMode = new System.Drawing.Point(0, 1);
            this.ultraTile3.TabIndex = 2;
            // 
            // ultraDayView1
            // 
            this.ultraDayView1.CalendarInfo = this.ultraCalendarInfo1;
            this.ultraDayView1.CalendarLook = this.ultraCalendarLook1;
            this.ultraDayView1.Location = new System.Drawing.Point(0, 18);
            this.ultraDayView1.Name = "ultraDayView1";
            this.ultraDayView1.Size = new System.Drawing.Size(505, 494);
            this.ultraDayView1.TabIndex = 0;
            this.ultraDayView1.Text = "ultraDayView1";
            // 
            // ultraMonthViewSingle1
            // 
            this.ultraMonthViewSingle1.CalendarInfo = this.ultraCalendarInfo1;
            this.ultraMonthViewSingle1.CalendarLook = this.ultraCalendarLook1;
            this.ultraMonthViewSingle1.Location = new System.Drawing.Point(0, 18);
            this.ultraMonthViewSingle1.Name = "ultraMonthViewSingle1";
            this.ultraMonthViewSingle1.Size = new System.Drawing.Size(180, 233);
            this.ultraMonthViewSingle1.TabIndex = 0;
            // 
            // ultraWeekView1
            // 
            this.ultraWeekView1.CalendarInfo = this.ultraCalendarInfo1;
            this.ultraWeekView1.CalendarLook = this.ultraCalendarLook1;
            this.ultraWeekView1.Location = new System.Drawing.Point(0, 18);
            this.ultraWeekView1.Name = "ultraWeekView1";
            this.ultraWeekView1.Size = new System.Drawing.Size(180, 233);
            this.ultraWeekView1.TabIndex = 0;
            // 
            // FrmCalendario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(725, 532);
            this.Controls.Add(this.ultraTilePanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmCalendario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calendário";
            ((System.ComponentModel.ISupportInitialize)(this.ultraTilePanel1)).EndInit();
            this.ultraTilePanel1.ResumeLayout(false);
            this.ultraTile1.ResumeLayout(false);
            this.ultraTile2.ResumeLayout(false);
            this.ultraTile3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraDayView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraMonthViewSingle1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraWeekView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinSchedule.UltraCalendarInfo ultraCalendarInfo1;
        private Infragistics.Win.UltraWinSchedule.UltraCalendarLook ultraCalendarLook1;
        private Infragistics.Win.Misc.UltraTilePanel ultraTilePanel1;
        private Infragistics.Win.Misc.UltraTile ultraTile1;
        private Infragistics.Win.Misc.UltraTile ultraTile2;
        private Infragistics.Win.Misc.UltraTile ultraTile3;
        private Infragistics.Win.UltraWinSchedule.UltraDayView ultraDayView1;
        private Infragistics.Win.UltraWinSchedule.UltraWeekView ultraWeekView1;
        private Infragistics.Win.UltraWinSchedule.UltraMonthViewSingle ultraMonthViewSingle1;

    }
}