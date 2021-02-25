﻿namespace Edgecam_Manager
 {
     partial class FrmFabrica
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
             Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
             Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
             Infragistics.Win.UltraWinSchedule.DayOfWeek dayOfWeek1 = new Infragistics.Win.UltraWinSchedule.DayOfWeek(System.DayOfWeek.Sunday);
             Infragistics.Win.UltraWinSchedule.DayOfWeek dayOfWeek2 = new Infragistics.Win.UltraWinSchedule.DayOfWeek(System.DayOfWeek.Monday);
             Infragistics.Win.UltraWinSchedule.DayOfWeek dayOfWeek3 = new Infragistics.Win.UltraWinSchedule.DayOfWeek(System.DayOfWeek.Tuesday);
             Infragistics.Win.UltraWinSchedule.DayOfWeek dayOfWeek4 = new Infragistics.Win.UltraWinSchedule.DayOfWeek(System.DayOfWeek.Wednesday);
             Infragistics.Win.UltraWinSchedule.DayOfWeek dayOfWeek5 = new Infragistics.Win.UltraWinSchedule.DayOfWeek(System.DayOfWeek.Thursday);
             Infragistics.Win.UltraWinSchedule.DayOfWeek dayOfWeek6 = new Infragistics.Win.UltraWinSchedule.DayOfWeek(System.DayOfWeek.Friday);
             Infragistics.Win.UltraWinSchedule.DayOfWeek dayOfWeek7 = new Infragistics.Win.UltraWinSchedule.DayOfWeek(System.DayOfWeek.Saturday);
             Infragistics.Win.UltraWinSchedule.DateInterval dateInterval1 = new Infragistics.Win.UltraWinSchedule.DateInterval();
             Infragistics.Win.UltraWinSchedule.TimeInterval timeInterval1 = new Infragistics.Win.UltraWinSchedule.TimeInterval();
             Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
             Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
             Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
             Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
             System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFabrica));
             this.ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
             this.ultraDayView1 = new Infragistics.Win.UltraWinSchedule.UltraDayView();
             this.ultraCalendarInfo1 = new Infragistics.Win.UltraWinSchedule.UltraCalendarInfo(this.components);
             this.ultraCalendarLook1 = new Infragistics.Win.UltraWinSchedule.UltraCalendarLook(this.components);
             this.ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
             this.ultraWeekView = new Infragistics.Win.UltraWinSchedule.UltraWeekView();
             this.ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
             this.ultraMonthCalendarView = new Infragistics.Win.UltraWinSchedule.UltraMonthViewSingle();
             this.ultraTabPageControl4 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
             this.ultraTimelineView = new Infragistics.Win.UltraWinSchedule.UltraTimelineView();
             this.panel1 = new System.Windows.Forms.Panel();
             this.groupBox1 = new System.Windows.Forms.GroupBox();
             this.dtDataSelecao = new System.Windows.Forms.DateTimePicker();
             this.label2 = new System.Windows.Forms.Label();
             this.cbMaquinas = new System.Windows.Forms.ComboBox();
             this.label1 = new System.Windows.Forms.Label();
             this.ultraTabControl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
             this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
             this.ultraTabPageControl1.SuspendLayout();
             ((System.ComponentModel.ISupportInitialize)(this.ultraDayView1)).BeginInit();
             this.ultraTabPageControl2.SuspendLayout();
             ((System.ComponentModel.ISupportInitialize)(this.ultraWeekView)).BeginInit();
             this.ultraTabPageControl3.SuspendLayout();
             ((System.ComponentModel.ISupportInitialize)(this.ultraMonthCalendarView)).BeginInit();
             this.ultraTabPageControl4.SuspendLayout();
             ((System.ComponentModel.ISupportInitialize)(this.ultraTimelineView)).BeginInit();
             this.panel1.SuspendLayout();
             this.groupBox1.SuspendLayout();
             ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl)).BeginInit();
             this.ultraTabControl.SuspendLayout();
             this.SuspendLayout();
             // 
             // ultraTabPageControl1
             // 
             this.ultraTabPageControl1.Controls.Add(this.ultraDayView1);
             this.ultraTabPageControl1.Location = new System.Drawing.Point(91, 1);
             this.ultraTabPageControl1.Name = "ultraTabPageControl1";
             this.ultraTabPageControl1.Size = new System.Drawing.Size(913, 462);
             // 
             // ultraDayView1
             // 
             this.ultraDayView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
             | System.Windows.Forms.AnchorStyles.Left)
             | System.Windows.Forms.AnchorStyles.Right)));
             this.ultraDayView1.CalendarInfo = this.ultraCalendarInfo1;
             this.ultraDayView1.CalendarLook = this.ultraCalendarLook1;
             this.ultraDayView1.GroupingStyle = Infragistics.Win.UltraWinSchedule.DayViewGroupingStyle.DateWithinOwner;
             this.ultraDayView1.Location = new System.Drawing.Point(29, 24);
             this.ultraDayView1.Name = "ultraDayView1";
             appearance1.BackColor = System.Drawing.Color.Red;
             this.ultraDayView1.NonWorkingHourTimeSlotAppearance = appearance1;
             this.ultraDayView1.Size = new System.Drawing.Size(861, 413);
             this.ultraDayView1.TabIndex = 0;
             this.ultraDayView1.Text = "ultraDayView1";
             appearance2.BackColor = System.Drawing.Color.White;
             this.ultraDayView1.WorkingHourTimeSlotAppearance = appearance2;
             // 
             // ultraCalendarInfo1
             // 
             this.ultraCalendarInfo1.AllowRecurringAppointments = true;
             this.ultraCalendarInfo1.AppointmentActionsEnabled = false;
             this.ultraCalendarInfo1.DataBindingsForAppointments.BindingContextControl = this;
             this.ultraCalendarInfo1.DataBindingsForOwners.BindingContextControl = this;
             dayOfWeek1.Enabled = false;
             dayOfWeek1.WorkDayEndTime = new System.DateTime(2001, 12, 31, 18, 0, 0, 0);
             dayOfWeek2.WorkDayEndTime = new System.DateTime(2001, 12, 31, 18, 0, 0, 0);
             dayOfWeek3.WorkDayEndTime = new System.DateTime(2001, 12, 31, 18, 0, 0, 0);
             dayOfWeek4.WorkDayEndTime = new System.DateTime(2001, 12, 31, 18, 0, 0, 0);
             dayOfWeek5.WorkDayEndTime = new System.DateTime(2001, 12, 31, 18, 0, 0, 0);
             dayOfWeek6.WorkDayEndTime = new System.DateTime(2001, 12, 31, 18, 0, 0, 0);
             dayOfWeek7.Enabled = false;
             dayOfWeek7.WorkDayEndTime = new System.DateTime(2001, 12, 31, 18, 0, 0, 0);
             this.ultraCalendarInfo1.DaysOfWeek.Add(dayOfWeek1);
             this.ultraCalendarInfo1.DaysOfWeek.Add(dayOfWeek2);
             this.ultraCalendarInfo1.DaysOfWeek.Add(dayOfWeek3);
             this.ultraCalendarInfo1.DaysOfWeek.Add(dayOfWeek4);
             this.ultraCalendarInfo1.DaysOfWeek.Add(dayOfWeek5);
             this.ultraCalendarInfo1.DaysOfWeek.Add(dayOfWeek6);
             this.ultraCalendarInfo1.DaysOfWeek.Add(dayOfWeek7);
             this.ultraCalendarInfo1.FirstDayOfWeek = Infragistics.Win.UltraWinSchedule.FirstDayOfWeek.Sunday;
             this.ultraCalendarInfo1.BeforeDisplayAppointmentDialog += new Infragistics.Win.UltraWinSchedule.DisplayAppointmentDialogEventHandler(this.ultraCalendarInfo1_BeforeDisplayAppointmentDialog);
             // 
             // ultraTabPageControl2
             // 
             this.ultraTabPageControl2.Controls.Add(this.ultraWeekView);
             this.ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
             this.ultraTabPageControl2.Name = "ultraTabPageControl2";
             this.ultraTabPageControl2.Size = new System.Drawing.Size(913, 462);
             // 
             // ultraWeekView
             // 
             this.ultraWeekView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
             | System.Windows.Forms.AnchorStyles.Left)
             | System.Windows.Forms.AnchorStyles.Right)));
             this.ultraWeekView.CalendarInfo = this.ultraCalendarInfo1;
             this.ultraWeekView.CalendarLook = this.ultraCalendarLook1;
             this.ultraWeekView.Location = new System.Drawing.Point(33, 30);
             this.ultraWeekView.Name = "ultraWeekView";
             this.ultraWeekView.Size = new System.Drawing.Size(844, 410);
             this.ultraWeekView.TabIndex = 0;
             this.ultraWeekView.TimeDisplayStyle = Infragistics.Win.UltraWinSchedule.TimeDisplayStyleEnum.Time24Hour;
             // 
             // ultraTabPageControl3
             // 
             this.ultraTabPageControl3.Controls.Add(this.ultraMonthCalendarView);
             this.ultraTabPageControl3.Location = new System.Drawing.Point(-10000, -10000);
             this.ultraTabPageControl3.Name = "ultraTabPageControl3";
             this.ultraTabPageControl3.Size = new System.Drawing.Size(913, 462);
             // 
             // ultraMonthCalendarView
             // 
             this.ultraMonthCalendarView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
             | System.Windows.Forms.AnchorStyles.Left)
             | System.Windows.Forms.AnchorStyles.Right)));
             this.ultraMonthCalendarView.CalendarInfo = this.ultraCalendarInfo1;
             this.ultraMonthCalendarView.CalendarLook = this.ultraCalendarLook1;
             this.ultraMonthCalendarView.Location = new System.Drawing.Point(26, 20);
             this.ultraMonthCalendarView.Name = "ultraMonthCalendarView";
             this.ultraMonthCalendarView.Size = new System.Drawing.Size(857, 423);
             this.ultraMonthCalendarView.TabIndex = 0;
             // 
             // ultraTabPageControl4
             // 
             this.ultraTabPageControl4.Controls.Add(this.ultraTimelineView);
             this.ultraTabPageControl4.Location = new System.Drawing.Point(-10000, -10000);
             this.ultraTabPageControl4.Name = "ultraTabPageControl4";
             this.ultraTabPageControl4.Size = new System.Drawing.Size(913, 462);
             // 
             // ultraTimelineView
             // 
             this.ultraTimelineView.AdditionalIntervals.Add(dateInterval1);
             this.ultraTimelineView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
             | System.Windows.Forms.AnchorStyles.Left)
             | System.Windows.Forms.AnchorStyles.Right)));
             this.ultraTimelineView.CalendarInfo = this.ultraCalendarInfo1;
             this.ultraTimelineView.CalendarLook = this.ultraCalendarLook1;
             this.ultraTimelineView.ColumnWidth = 0;
             this.ultraTimelineView.Location = new System.Drawing.Point(35, 16);
             this.ultraTimelineView.Name = "ultraTimelineView";
             this.ultraTimelineView.OwnerGroupingStyle = Infragistics.Win.UltraWinSchedule.TimelineViewOwnerGroupingStyle.Merged;
             timeInterval1.IntervalUnits = Infragistics.Win.UltraWinSchedule.TimeIntervalUnits.Hours;
             this.ultraTimelineView.PrimaryInterval = timeInterval1;
             this.ultraTimelineView.Size = new System.Drawing.Size(860, 443);
             this.ultraTimelineView.TabIndex = 0;
             // 
             // panel1
             // 
             this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
             | System.Windows.Forms.AnchorStyles.Left)
             | System.Windows.Forms.AnchorStyles.Right)));
             this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
             this.panel1.Controls.Add(this.groupBox1);
             this.panel1.Controls.Add(this.ultraTabControl);
             this.panel1.Location = new System.Drawing.Point(0, 0);
             this.panel1.Name = "panel1";
             this.panel1.Size = new System.Drawing.Size(1031, 669);
             this.panel1.TabIndex = 0;
             // 
             // groupBox1
             // 
             this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
             | System.Windows.Forms.AnchorStyles.Right)));
             this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
             this.groupBox1.Controls.Add(this.dtDataSelecao);
             this.groupBox1.Controls.Add(this.label2);
             this.groupBox1.Controls.Add(this.cbMaquinas);
             this.groupBox1.Controls.Add(this.label1);
             this.groupBox1.Location = new System.Drawing.Point(12, 12);
             this.groupBox1.Name = "groupBox1";
             this.groupBox1.Size = new System.Drawing.Size(1007, 173);
             this.groupBox1.TabIndex = 0;
             this.groupBox1.TabStop = false;
             this.groupBox1.Text = "Filtros";
             // 
             // dtDataSelecao
             // 
             this.dtDataSelecao.Location = new System.Drawing.Point(258, 46);
             this.dtDataSelecao.Name = "dtDataSelecao";
             this.dtDataSelecao.Size = new System.Drawing.Size(219, 20);
             this.dtDataSelecao.TabIndex = 5;
             this.dtDataSelecao.ValueChanged += new System.EventHandler(this.dtDataSelecao_ValueChanged);
             // 
             // label2
             // 
             this.label2.AutoSize = true;
             this.label2.Location = new System.Drawing.Point(16, 29);
             this.label2.Name = "label2";
             this.label2.Size = new System.Drawing.Size(104, 13);
             this.label2.TabIndex = 4;
             this.label2.Text = "Centros de trabalhos";
             // 
             // cbMaquinas
             // 
             this.cbMaquinas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
             this.cbMaquinas.FormattingEnabled = true;
             this.cbMaquinas.Location = new System.Drawing.Point(19, 45);
             this.cbMaquinas.Name = "cbMaquinas";
             this.cbMaquinas.Size = new System.Drawing.Size(206, 21);
             this.cbMaquinas.TabIndex = 3;
             this.cbMaquinas.SelectedIndexChanged += new System.EventHandler(this.cbMaquinas_SelectedIndexChanged);
             // 
             // label1
             // 
             this.label1.AutoSize = true;
             this.label1.Location = new System.Drawing.Point(255, 16);
             this.label1.Name = "label1";
             this.label1.Size = new System.Drawing.Size(161, 26);
             this.label1.TabIndex = 1;
             this.label1.Text = "Selecione um dia para visualizar \r\ncarga máquina:";
             // 
             // ultraTabControl
             // 
             this.ultraTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
             | System.Windows.Forms.AnchorStyles.Left)
             | System.Windows.Forms.AnchorStyles.Right)));
             this.ultraTabControl.Controls.Add(this.ultraTabSharedControlsPage1);
             this.ultraTabControl.Controls.Add(this.ultraTabPageControl1);
             this.ultraTabControl.Controls.Add(this.ultraTabPageControl2);
             this.ultraTabControl.Controls.Add(this.ultraTabPageControl3);
             this.ultraTabControl.Controls.Add(this.ultraTabPageControl4);
             this.ultraTabControl.Location = new System.Drawing.Point(12, 191);
             this.ultraTabControl.Name = "ultraTabControl";
             this.ultraTabControl.SharedControlsPage = this.ultraTabSharedControlsPage1;
             this.ultraTabControl.Size = new System.Drawing.Size(1007, 466);
             this.ultraTabControl.TabIndex = 1;
             this.ultraTabControl.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.LeftTop;
             ultraTab1.TabPage = this.ultraTabPageControl1;
             ultraTab1.Text = "Diário";
             ultraTab2.TabPage = this.ultraTabPageControl2;
             ultraTab2.Text = "Semanal";
             ultraTab3.TabPage = this.ultraTabPageControl3;
             ultraTab3.Text = "Mensal";
             ultraTab4.TabPage = this.ultraTabPageControl4;
             ultraTab4.Text = "Linha do tempo";
             this.ultraTabControl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab1,
            ultraTab2,
            ultraTab3,
            ultraTab4});
             this.ultraTabControl.TextOrientation = Infragistics.Win.UltraWinTabs.TextOrientation.Horizontal;
             // 
             // ultraTabSharedControlsPage1
             // 
             this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
             this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
             this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(913, 462);
             // 
             // FrmFabrica
             // 
             this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
             this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
             this.ClientSize = new System.Drawing.Size(1031, 669);
             this.Controls.Add(this.panel1);
             this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
             this.Name = "FrmFabrica";
             this.ultraTabPageControl1.ResumeLayout(false);
             ((System.ComponentModel.ISupportInitialize)(this.ultraDayView1)).EndInit();
             this.ultraTabPageControl2.ResumeLayout(false);
             ((System.ComponentModel.ISupportInitialize)(this.ultraWeekView)).EndInit();
             this.ultraTabPageControl3.ResumeLayout(false);
             ((System.ComponentModel.ISupportInitialize)(this.ultraMonthCalendarView)).EndInit();
             this.ultraTabPageControl4.ResumeLayout(false);
             ((System.ComponentModel.ISupportInitialize)(this.ultraTimelineView)).EndInit();
             this.panel1.ResumeLayout(false);
             this.groupBox1.ResumeLayout(false);
             this.groupBox1.PerformLayout();
             ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl)).EndInit();
             this.ultraTabControl.ResumeLayout(false);
             this.ResumeLayout(false);

         }

         #endregion

         private System.Windows.Forms.Panel panel1;
         private System.Windows.Forms.GroupBox groupBox1;
         private Infragistics.Win.UltraWinTabControl.UltraTabControl ultraTabControl;
         private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
         private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl1;
         private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl2;
         private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl3;
         private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl4;
         private Infragistics.Win.UltraWinSchedule.UltraWeekView ultraWeekView;
         private Infragistics.Win.UltraWinSchedule.UltraMonthViewSingle ultraMonthCalendarView;
         private System.Windows.Forms.Label label1;
         private System.Windows.Forms.Label label2;
         private System.Windows.Forms.ComboBox cbMaquinas;
         private System.Windows.Forms.DateTimePicker dtDataSelecao;
         private Infragistics.Win.UltraWinSchedule.UltraTimelineView ultraTimelineView;
         private Infragistics.Win.UltraWinSchedule.UltraDayView ultraDayView1;
         private Infragistics.Win.UltraWinSchedule.UltraCalendarInfo ultraCalendarInfo1;
         private Infragistics.Win.UltraWinSchedule.UltraCalendarLook ultraCalendarLook1;
     }
 }