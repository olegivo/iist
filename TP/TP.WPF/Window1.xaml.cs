﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using DMS.Common.Events;

namespace TP.WPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

	public partial class Window1: Window

    {
		public ChannelController channelController1 = new ChannelController();
        public Window1()
        {
            InitializeComponent(); 
            channelController1.AutoSubscribeChannels = true;
            //channelController1.CanRegisterChanged += new System.EventHandler(this.channelController1_CanRegisterChanged);
            channelController1.InitProvider("HighLevelClient");
            channelController1.NeedProtocol += channelController1_NeedProtocol;
            channelController1.HasReadChannel += channelController1_HasReadChannel;
            channelController1.CanRegister = true;
        }


        void channelController1_HasReadChannel(object sender, DataEventArgs e)
        {
            float value = Convert.ToSingle(e.Message.Value);
            int channelId = e.Message.LogicalChannelId;
            switch (channelId)
            {
            //    case 1:
            //        ucDrumTypeFurnace1.T1 = value;
            //        break; //TП1	температура в циклонной вихревой топке
            //    case 2:
            //        ucDrumTypeFurnace1.T2 = value;
            //        break; //TП2	температура в загрузочной системе
            //    case 3:
            //        break; //TП3	температура в камере дожигания
            //    case 4:
            //        ucAllHeatExchanger1.Temperature_TP4 = value;
            //        break; //TР4	температура в теплообменнике ТО1
            //    case 5:
            //        ucAllHeatExchanger1.Temperature_TP5 = value;
            //        break; //TР5	температура в теплообменнике ТО2
                  case 6:
                    FinishCleaning.Temperature_TC6 = value;
                    break; //TС6	температура перед рукавным фильтром
                  case 7:
                    FinishCleaning.Temperature_TC7 = value;
                    break; //TС7	температура перед дымососом
            //    case 8:
            //        ucDrumTypeFurnace1.T8 = value;
            //        break; //TС8	температура воды в системе охлаждения
            //    case 9:
            //        break; //Р	разрежение в камере дожигания
            //    case 10:
            //        ucCyclonAndScrubber1.Ph1 = value;
            //        break; //рН1	уровень рН в СФ1
            //    case 11:
            //        ucCyclonAndScrubber1.Ph2 = value;
            //        break; //рН2	уровень рН в СФ2
            //    case 12:
            //        ucDrumTypeFurnace1.S = value;
            //        break; //S	скорость вращения печи
            //    case 13:
            //        ucDrumTypeFurnace1.DU9 = value;
            //        ucChart1.AddDataChart(channelId, Convert.ToInt32(value));
            //        break; //ДУ-9	уровень отходов в бункере
                  case 14:
                    ReheatChamber.Temperature = value*100;
                      //this.ReheatChamber.Temperature=value*100;
					
                    //ucReheatChamber1.Level11 = value;
                    //ucChart1.AddDataChart(channelId, Convert.ToInt32(value));
                    break; //ДУ-11	уровень в РТ
            //    case 15:
            //        ucReheatChamber1.Level1 = value;
            //        ucChart1.AddDataChart(channelId, Convert.ToInt32(value));
            //        break; //ДУ-1	уровень в НЕ
            //    case 16:
            //        ucReheatChamber1.Level4 = value;
            //        ucChart1.AddDataChart(channelId, Convert.ToInt32(value));
            //        break; //ДУ-4	уровень в РЕ
            //    case 17:
            //        ucCyclonAndScrubber1.Level10 = value;
            //        ucChart1.AddDataChart(channelId, Convert.ToInt32(value));
            //        break; //ДУ-10	уровень в СБ
            //    case 18:
            //        ucAllHeatExchanger1.Concentration_O2 = value;
            //        break; //Г-О2	концентрация газа О2
            //    case 19:
            //        ucAllHeatExchanger1.Concentration_CO = value;
            //        break; //Г-СО	концентрация газа СО
                  case 20:
                    FinishCleaning.GasConcentration_O2 = value;
                    break; //Г-О2	концентрация газа О2
                  case 21:
                    FinishCleaning.GasConcentration_CO = value;
                    break; //Г-СО	концентрация газа СО
                  case 22:
                    FinishCleaning.GasConcentration_SO2 = value;
                    break; //Г-SО2	концентрация газа SО2
                  case 23:
                    FinishCleaning.GasConcentration_NO = value;
                    break; //Г-NО	концентрация газа NО
                  case 24:
                    FinishCleaning.GasConcentration_NO2 = value;
                    break; //Г-NО2	концентрация газа NО2
            }
        }

        void channelController1_NeedProtocol(object sender, EventArgs e)
        {
            Protocol(sender);
        }

        private void Protocol(object sender)
        {
            if (sender is double || sender is string)
            {
                string s = string.Format("{0}\t{1}{2}", DateTime.Now, sender, Environment.NewLine);

                SetText(this.textBox1, s);
            }
        }
        /// <summary>
        /// Запись протокола с обработкой события, произошедшего в другом потоке
        /// </summary>
        /// <param name="info">TextBox</param>
        /// <param name="s">string</param>
        private delegate void StDelegate(TextBox info, string s);
        private void SetText(TextBox info, string s) 
        {
            if (Dispatcher.Thread != Thread.CurrentThread)
            {
                StDelegate ddd = SetText;
                Dispatcher.Invoke(ddd, new object[]{info,s});
            }
            else 
            {
                info.AppendText(s);            
            }
        }

        private void ucConnectionWindow_NeedRegister(object sender, EventArgs e)
        {
            channelController1.Register();
        }
		
        private void Menu_help_Click(object sender, System.Windows.RoutedEventArgs e)
        {
			MessageBox.Show("Две вещи действительно бесконечны: вселенная и  человеческая глупость.");
        }

        private void Menu_exit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	this.Close();
        }

        private void Menu_unregister_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	this.channelController1.Unregister();
        }

        private void Menu_register_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	this.channelController1.Register();
        }

        private void Path_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        	// TODO: Add event handler implementation here.
			base.OnMouseDown(e);
			DragMove();
        }

        private void CloseButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
			this.Close();
        }

        private void MaximizeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
			this.WindowState=(this.WindowState==System.Windows.WindowState.Maximized)?System.Windows.WindowState.Normal:System.Windows.WindowState.Maximized;
        }

        private void MinimizeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
			this.WindowState=System.Windows.WindowState.Minimized;
        }

        private void FinishCleaning_SendControlMessage(object sender, TP.WPF.SendControlMessageEventArgs e)
        {
            channelController1.WriteChannel(e.ChannelId, e.Value);
        }
        
        //private void channelController1_CanRegisterChanged(object sender, EventArgs e)
        //{
        //    sbUnregister.Enabled = !(sbRegister.Enabled = channelController1.CanRegister);
        //}

    }
}