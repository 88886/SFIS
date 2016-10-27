using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Collections;
using System.Xml;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading;
using RefWebService_BLL;

namespace SFIS_V2
{
    public partial class CreateRoute : Office2007Form// Form
    {
        public CreateRoute(MainParent _frm)
        {
            InitializeComponent();
            mFrm = _frm;
        }

        /// <summary>
        /// 存储途程代码和名字 20131220
        /// </summary>
        Dictionary<string, string> SetCraftName = new Dictionary<string, string>();

        #region 画流程图相关变量
        private System.Windows.Forms.ContextMenu menuDelSet;
        private System.Windows.Forms.MenuItem menuDelStatus;
        private System.Windows.Forms.MenuItem menuSaveXml;
        private System.Windows.Forms.ContextMenu menuAdd;
        private System.Windows.Forms.MenuItem menuLoadXml;
        private System.Windows.Forms.MenuItem menuSetStatus;
        private System.Windows.Forms.MenuItem menuSetRouteStart;
        private System.Windows.Forms.MenuItem menuSetRouteEnd;
        private System.Windows.Forms.MenuItem menuClearRouteSet;
        /// <summary>
        /// 存放所有status对象
        /// </summary>
        //private List<FlowStatus> lsStatus = new List<FlowStatus>();
        /// <summary>
        /// 存放所有status对象
        /// </summary>
        private Dictionary<string, FlowStatus> lsStatus = new Dictionary<string, FlowStatus>();
        /// <summary>
        /// 存放所有Disposal对象
        /// </summary>
        private List<FlowDisposal> lsDisposal = new List<FlowDisposal>();
        /// <summary>
        /// 在Form中被选中的唯一状态
        /// </summary>
        public FlowStatus gStatus;
        /// <summary>
        /// 画线时的from 和 to
        /// </summary>
        private FlowStatus gFrom = null, gTo = null;
        /// <summary>
        /// 鼠标按下时坐标x值
        /// </summary>
        private int intMouseX;
        /// <summary>
        /// 鼠标按下时坐标Y值
        /// </summary>
        private int intMouseY;
        /// <summary>
        /// ctrl键是否按下
        /// </summary>
        private bool blCtrlDown = false;
        /// <summary>
        /// 状态对象矩形的宽度
        /// </summary>
        private int intStatusW = 90;
        /// <summary>
        /// 状态对象矩形的高度
        /// </summary>
        private int intStautsH = 45;

        /// <summary>
        /// 环节类型ID,STATUS_TYPE_ID
        /// </summary>
        private enum eStatusTypeId
        {
            开始 = 0,
            结束 = 1,
            普通 = 2,
            子流 = 3,
            自动 = 4,
            分流 = 5,
            合流 = 6,
            转流程 = 7
        }

        eStatusTypeId _estatustypeid;
        #endregion

        #region 内部成员变量
        private readonly string strFnumName = "RouteEdit";
        MainParent mFrm;
        /// <summary>
        /// 保存流程编号
        /// </summary>
        public string gRoutegroupId { get; set; }
        /// <summary>
        /// 保存流程名称
        /// </summary>
        public string gRoutegroupname { get; set; }
        /// <summary>
        /// 临时datatable
        /// </summary>
        DataTable dtlsallcip = new DataTable();

        /// <summary>
        /// 流程的各种状态
        /// </summary>
        private enum eRouteGroupType
        {
            /// <summary>
            /// 新建的流程
            /// </summary>
            NewRoute,
            /// <summary>
            /// 更新的流程(已经有工单使用)
            /// </summary>
            UpdateRoute,
            /// <summary>
            /// 当前是空状态
            /// </summary>
            NullRoute,
        }
        /// <summary>
        /// 跟踪流程状态
        /// </summary>
        eRouteGroupType mRoutegrouptype = eRouteGroupType.NullRoute;
        /// <summary>
        /// 区分是选择了工艺还是选择了流程(默认为假:选择工艺)
        /// </summary>
        private bool isCraftOrRoute = false;

        /// <summary>
        /// 记录途程的排序和每个途程的项目和参数
        /// </summary>
        private Dictionary<string, CraftItemInfo> lsCraftItemInfo = new Dictionary<string, CraftItemInfo>();
        /// <summary>
        /// 用来保存传过来的表格
        /// </summary>
        public DataTable gdatatable { get; set; }
        #endregion
        #region 画流程图相关函数
        /// <summary>
        /// 添加一个对象
        /// </summary>
        private void addStatus(string statusId, string statusname)
        {
            int j = 0;
            foreach (FlowStatus objTempS in lsStatus.Values)
            {
                if (objTempS.blDisplay == false)
                {
                    objTempS._x = this.intMouseX;
                    objTempS._y = this.intMouseY;
                    objTempS.intStatusTypeId = (int)_estatustypeid;
                    objTempS.blDisplay = true;
                    j++;
                    objTempS.dblStatusId = statusId + "_" + j.ToString();
                    objTempS.strStatusName = statusname;
                    repaint();
                    return;
                }
            }

            FlowStatus objStatus = new FlowStatus(intMouseX, intMouseY, this.intStatusW, this.intStautsH);
            objStatus.intStatusTypeId = (int)_estatustypeid;
            int intI = lsStatus.Count + 1;
            objStatus.dblStatusId = statusId + "_" + intI.ToString();
            objStatus.strStatusName = statusname;
            this.lsStatus.Add(objStatus.dblStatusId, objStatus);
            repaint();
        }
        /// <summary>
        /// 画图
        /// </summary>
        private void repaint()
        {
            Graphics g = this.flowpanel.CreateGraphics();
            //先将图形所有区域用白色填充				
            Pen WhitePen = new Pen(Color.White);
            g.DrawRectangle(WhitePen, 0, 0, this.flowpanel.Width, this.flowpanel.Height);
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, this.flowpanel.Width, this.flowpanel.Height);
            //填充完毕
            foreach (FlowStatus objStatus in lsStatus.Values)//画代表状态的矩形	   
            {
                if (objStatus.blDisplay)//是否已经被删除,如果没有blDisplay==true则画图显示
                {
                    int intType = objStatus.intStatusTypeId;
                    int x1 = objStatus._x;
                    int y1 = objStatus._y;
                    int w1 = objStatus._w;
                    int h1 = objStatus._h;
                    int cornerRadius = 5;
                    string strName = objStatus.strStatusName;
                    string strId = objStatus.dblStatusId;

                    Pen penBlack = new Pen(Color.FromArgb(150, 150, 150), 3);


                    //画阴影部分
                    MyDrawRectangle.FillRoundRectangle(g, new SolidBrush(Color.FromArgb(100, 100, 100)), new Rectangle
                    {
                        X = x1 + 6,
                        Y = y1 + 6,
                        Width = w1,
                        Height = h1
                    }, cornerRadius);

                    //画整个矩形
                    MyDrawRectangle.DrawRoundRectangle(g, penBlack, new Rectangle
                    {
                        X = x1,
                        Y = y1,
                        Width = w1,
                        Height = h1
                    }, cornerRadius);

                    //将整个矩形填充一次
                    MyDrawRectangle.FillRoundRectangle(g, new SolidBrush(Color.Blue), new Rectangle
                    {
                        X = x1,
                        Y = y1,
                        Width = w1,
                        Height = h1
                    }, cornerRadius);

                    if (objStatus.intStatusTypeId == 0) //开始
                    {
                        MyDrawRectangle.FillRoundRectangle(g, new SolidBrush(Color.Yellow), new Rectangle
                        {
                            X = x1,
                            Y = y1,
                            Width = w1,
                            Height = Convert.ToInt32(h1 * 0.5)
                        }, cornerRadius);
                    }
                    else if (objStatus.intStatusTypeId == 1) //结束
                    {
                        MyDrawRectangle.FillRoundRectangle(g, new SolidBrush(Color.Green), new Rectangle
                        {
                            X = x1,
                            Y = y1,
                            Width = w1,
                            Height = Convert.ToInt32(h1 * 0.5)
                        }, cornerRadius);
                    }
                    else  //其他
                    {
                        MyDrawRectangle.FillRoundRectangle(g, new SolidBrush(Color.Orange), new Rectangle
                        {
                            X = x1,
                            Y = y1,
                            Width = w1,
                            Height = Convert.ToInt32(h1 * 0.5)
                        }, cornerRadius);
                    }

                    MyDrawRectangle.FillRoundRectangle(g, new SolidBrush(Color.Blue), new Rectangle
                    {
                        X = x1,
                        Y = y1 + Convert.ToInt32((h1 * 0.6)),
                        Width = w1,
                        Height = Convert.ToInt32(h1 * 0.3)
                    }, cornerRadius);

                    Font drawFont = new Font("宋体", 9);
                    Font drawId = new Font("宋体", 7);
                    StringFormat drawFormat = new StringFormat();

                    drawFormat.Alignment = StringAlignment.Center;
                    SolidBrush drawBrush = new SolidBrush(Color.Black);

                    RectangleF drawRect = new RectangleF(x1, y1 + 2, w1, Convert.ToInt32(h1 * 0.6));
                    RectangleF drawRect2 = new RectangleF(x1, y1 + 2 + Convert.ToInt32(h1 * 0.6), w1, Convert.ToInt32(h1 * 0.4));
                    g.DrawString(strName, drawFont, drawBrush, drawRect, drawFormat);
                    g.DrawString(strId, drawId, new SolidBrush(Color.White), drawRect2, drawFormat);
                }//end if 
            }//end for	
            g.Dispose();//画代表状态的矩形	    
            this.drawAllDisposal();//画线   
        }
        /// <summary>
        /// 画所有的线
        /// </summary>
        private void drawAllDisposal()
        {
            foreach (FlowDisposal objDisp in lsDisposal)
            {
                if (objDisp.blDisplay)
                {
                    Graphics g = this.flowpanel.CreateGraphics();
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    Pen penBlack;

                    if (objDisp.gRoutetype == FlowDisposal.eRouteType.正常流程)
                        penBlack = new Pen(Color.FromArgb(69, 60, 100), 2) { StartCap = LineCap.Flat, EndCap = LineCap.ArrowAnchor };
                    else if (objDisp.gRoutetype == FlowDisposal.eRouteType.维修流程)
                        penBlack = new Pen(Color.Red, 2);
                    else
                        penBlack = new Pen(Color.FromArgb(163, 73, 164), 2) { StartCap = LineCap.Flat, EndCap = LineCap.ArrowAnchor };

                    //g.DrawLine(penBlack, objDisp.x1, objDisp.y1, objDisp.x1, objDisp.intCurY+2);
                    // g.DrawLine(penBlack, objDisp.x1, objDisp.intCurY, objDisp.intCurX+2, objDisp.intCurY);

                    Point _p = objDisp.GetPreShortPoint(new Point(objDisp.intCurX, objDisp.intCurY));
                    Point _pn = objDisp.GetCurShortPoint(new Point(objDisp.intCurX, objDisp.intCurY));

                    int xx = Math.Abs(objDisp.intCurX - _p.X);
                    int yy = Math.Abs(objDisp.intCurY - _p.Y);

                    int xx1 = Math.Abs(objDisp.intCurX - _pn.X);
                    int yy1 = Math.Abs(objDisp.intCurY - _pn.Y);

                    Point pT1 = new Point();
                    Point pT2 = new Point();

                    #region xxx
                    if (xx < yy)
                    {
                        pT1.X = objDisp.intCurX;
                        pT1.Y = _p.Y;
                    }
                    else
                    {
                        pT1.X = _p.X;
                        pT1.Y = objDisp.intCurY;
                    }

                    if (xx1 < yy1)
                    {
                        pT2.X = _pn.X;
                        pT2.Y = objDisp.intCurY;
                    }
                    else
                    {
                        pT2.X = objDisp.intCurX;
                        pT2.Y = _pn.Y;
                    }
                    #endregion

                    #region 直角线
                    g.DrawLine(penBlack, _p.X, _p.Y, pT1.X, pT1.Y);
                    g.DrawLine(penBlack, pT1.X, pT1.Y, objDisp.intCurX, objDisp.intCurY);
                    g.DrawLine(penBlack, objDisp.intCurX, objDisp.intCurY, pT2.X, pT2.Y);
                    drawArrowLine(pT2.X, pT2.Y, _pn.X, _pn.Y, objDisp.gRoutetype);
                    #endregion

                    #region 直线
                    // g.DrawLine(penBlack, _p.X, _p.Y, objDisp.intCurX, objDisp.intCurY);
                    //drawArrowLine(objDisp.intCurX, objDisp.intCurY, _pn.X, _pn.Y, objDisp.gRoutetype);
                    #endregion

                    g.Dispose();
                    this.flowpanel.Controls.Add(objDisp.lbDispName);
                }
            }
        }
        /// <summary>
        /// 画第2点带箭头的线
        /// </summary>
        /// <param name="xFrom"></param>
        /// <param name="yFrom"></param>
        /// <param name="xTo"></param>
        /// <param name="yTo"></param>
        private void drawArrowLine(int xFrom, int yFrom, int xTo, int yTo, FlowDisposal.eRouteType routetype)
        {
            Graphics g = this.flowpanel.CreateGraphics();
            Pen penBlack;

            if (routetype == FlowDisposal.eRouteType.正常流程)
                penBlack = new Pen(Color.FromArgb(69, 60, 100), 2);
            else if (routetype == FlowDisposal.eRouteType.维修流程)
                penBlack = new Pen(Color.Red, 2);
            else
                penBlack = new Pen(Color.FromArgb(163, 73, 164), 2);

            System.Drawing.Drawing2D.AdjustableArrowCap lineArrow = new AdjustableArrowCap(6, 6, true);
            penBlack.CustomEndCap = lineArrow;
            g.DrawLine(penBlack, xFrom, yFrom, xTo, yTo);

            #region 画箭头
            //int cxArrow = 12;
            //double angSlope = 0;
            //double angOffset = Math.PI / 8;
            //int xDiv = 0, yDiv = 0;
            //int xTD = 0, yTD = 0;
            //int xTF = xFrom - xTo;
            //int yTF = yFrom - yTo;
            //if (xTF == 0)
            //{//如果是垂直线没有atan2
            //    if (yTF > 0)
            //    {
            //        angSlope = Math.PI / 2;//90度
            //    }
            //    else
            //    {
            //        angSlope = -(Math.PI / 2);//负90度
            //    }
            //}
            //else
            //{
            //    angSlope = Math.Atan2(yTF, xTF);//正切弧度
            //}
            //xTD = (int)(cxArrow * Math.Cos(angSlope - angOffset));
            //yTD = (int)(cxArrow * Math.Sin(angSlope - angOffset));
            //xDiv = xTo + xTD;
            //yDiv = yTo + yTD;
            //g.DrawLine(penBlack, xTo, yTo, xDiv, yDiv);
            //xTD = (int)(cxArrow * Math.Cos(angSlope + angOffset));
            //yTD = (int)(cxArrow * Math.Sin(angSlope + angOffset));
            //xDiv = xTo + xTD;
            //yDiv = yTo + yTD;
            //g.DrawLine(penBlack, xTo, yTo, xDiv, yDiv);
            #endregion
            g.Dispose();
        }

        private void drawMouseObjs(MouseEventArgs ex, int intEvent)
        {
            int flag = 0;
            int statusSize = lsStatus.Count;
            if (statusSize == 0) return;
            #region ctrl键没有按下
            if (this.blCtrlDown == false)//
            {
                foreach (FlowStatus objStatus in lsStatus.Values)
                {
                    switch (intEvent)
                    {
                        case 1://mouseDown()
                            if (objStatus.contains(ex.X, ex.Y))
                            {
                                Graphics g = this.flowpanel.CreateGraphics();// this.CreateGraphics();
                                objStatus.last_x = objStatus._x - ex.X;
                                objStatus.last_y = objStatus._y - ex.Y;
                                objStatus.isMove = 1;
                                int x1 = objStatus._x;
                                int y1 = objStatus._y;
                                int w1 = objStatus._w;
                                int h1 = objStatus._h;
                                Pen penWhite = new Pen(Color.White, 2);
                                g.DrawRectangle(penWhite, x1, y1, w1, h1);
                                g.FillRectangle(new SolidBrush(Color.White), x1, y1, w1, h1);
                                g.FillRectangle(new SolidBrush(Color.White), x1 + 6, y1 + 6, w1, h1);
                                g.DrawRectangle(new Pen(Color.Black), x1, y1, w1, h1);
                                g.Dispose();
                                return;
                            }
                            break;

                        case 2://mouseMove()
                            if (objStatus.isMove == 1)
                            {
                                Graphics g = this.flowpanel.CreateGraphics();// this.CreateGraphics();
                                int x1 = objStatus._x;
                                int y1 = objStatus._y;
                                int w1 = objStatus._w;
                                int h1 = objStatus._h;
                                g.DrawRectangle(new Pen(Color.White), x1, y1, w1, h1);
                                objStatus.setLocation(ex.X, ex.Y);
                                this.checkStatus(objStatus);
                                int x2 = objStatus._x;
                                int y2 = objStatus._y;
                                Pen penBlack = new Pen(Color.Black);
                                g.DrawRectangle(penBlack, x2, y2, w1, h1);
                                g.Dispose();
                                return;
                            }
                            break;
                        case 3://mouseUp()
                            if (objStatus.isMove == 1)
                            {
                                updateLocation(ex, objStatus);
                                objStatus.isMove = 0;
                                repaint();
                                return;
                            }
                            break;
                    }//end switch
                }//end for
            }//end if
            #endregion
            #region ctrl键按下
            else if (this.blCtrlDown == true)
            {
                foreach (FlowStatus objStatus in lsStatus.Values)
                {
                    switch (intEvent)
                    {
                        case 1://mouseDown()
                            if (objStatus.contains(ex.X, ex.Y))
                            {
                                this.gFrom = objStatus;
                                return;
                            }
                            break;

                        case 2://mouseMove()

                            break;
                        case 3://mouseUp()	
                            this.blCtrlDown = false;
                            #region down
                            if (this.gFrom != null)
                            {
                                if (objStatus.contains(ex.X, ex.Y) && objStatus.dblStatusId != gFrom.dblStatusId)
                                {
                                    this.gTo = objStatus;
                                    #region 停用
                                    if (gFrom.intStatusTypeId == 1)
                                    {
                                        flag = 1;
                                        #region xx
                                        //MessageBoxEx.Show("结束状态只有流入的处理!");
                                        //gFrom = null;
                                        //gTo = null;
                                        //return;
                                        #endregion
                                    }
                                    else
                                    {
                                        if (gTo.intStatusTypeId == 0)
                                        {
                                            flag = 2;
                                            #region xxx
                                            //MessageBoxEx.Show("起始状态只有流出的处理!");
                                            //gFrom = null;
                                            //gTo = null;
                                            //return;
                                            #endregion
                                        }
                                    }
                                    #endregion
                                    int intDispCount = this.lsDisposal.Count;
                                    if (intDispCount != 0)
                                    {
                                        foreach (FlowDisposal objTemp in lsDisposal)
                                        {
                                            if (objTemp.dblPreStatusId == gFrom.dblStatusId &&
                                                objTemp.dblCurStatusId == gTo.dblStatusId)
                                            {
                                                MessageBoxEx.Show("该处理已经存在!");
                                                return;
                                            }

                                            if (objTemp.gRoutetype == FlowDisposal.eRouteType.正常流程)
                                            {
                                                if (objTemp.dblPreStatusId == gTo.dblStatusId &&
                                                    objTemp.dblCurStatusId == gFrom.dblStatusId)
                                                {
                                                    MessageBoxEx.Show("该操作会导致死循环\n\n操作非法", "提示");
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                    //如果处理对象列表中有已经被删除的处理对象，
                                    //将其在新位置重新显示
                                    for (int i = 0; i < this.lsDisposal.Count; i++)
                                    {
                                        FlowDisposal tempDisp = lsDisposal[i];
                                        if (tempDisp.blDisplay == false)
                                        {
                                            int x1 = gFrom._x + Convert.ToInt32(gFrom._w / 2);
                                            int y1 = gFrom._y + Convert.ToInt32(gFrom._h / 2);
                                            int x2 = gTo._x + Convert.ToInt32(gTo._w / 2);
                                            int y2 = gTo._y + Convert.ToInt32(gTo._h / 2);
                                            tempDisp.x1 = x1;
                                            tempDisp.y1 = y1;
                                            tempDisp.x2 = x2;
                                            tempDisp.y2 = y2;
                                            tempDisp.intCurX = Convert.ToInt32((x1 + x2) / 2);
                                            tempDisp.intCurY = Convert.ToInt32((y1 + y2) / 2);
                                            tempDisp.dblPreStatusId = gFrom.dblStatusId;
                                            tempDisp.dblCurStatusId = gTo.dblStatusId;
                                            tempDisp.blDisplay = true;
                                            int j = i + 1;
                                            if (flag == 1)
                                            {
                                                tempDisp.gRoutetype = FlowDisposal.eRouteType.维修流程;
                                                tempDisp.lbDispName.BackColor = Color.FromArgb(223, 86, 57);
                                            }
                                            else if (flag == 2)
                                            {
                                                tempDisp.gRoutetype = FlowDisposal.eRouteType.重测流程;
                                                tempDisp.lbDispName.BackColor = Color.FromArgb(163, 73, 164);
                                            }
                                            else
                                            {
                                                tempDisp.gRoutetype = FlowDisposal.eRouteType.正常流程;
                                                tempDisp.lbDispName.BackColor = Color.FromArgb(39, 50, 241);
                                            }
                                            flag = 0;

                                            ////上一个环节添加下一个节点
                                            //this.lsStatus[tempDisp.dblPreStatusId].NextStatusId.Add(tempDisp.dblCurStatusId);
                                            ////下一个环节添加上一个节点
                                            //this.lsStatus[tempDisp.dblCurStatusId].UpStatusId.Add(tempDisp.dblPreStatusId);

                                            tempDisp.dblDisposalId = Convert.ToDouble(j);
                                            tempDisp.strDisposalName = "流程" + Convert.ToString(j);
                                            tempDisp.lbDispName.Text = "流程" + Convert.ToString(j);
                                            tempDisp.lbDispName.Location =
                                                new System.Drawing.Point(tempDisp.intCurX - Convert.ToInt32(tempDisp.intlblw / 2), tempDisp.intCurY - Convert.ToInt32(tempDisp.intlblh / 2));
                                            tempDisp.lbDispName.Visible = true;
                                            tempDisp.aryStatusObjs = this.lsStatus;
                                            repaint();
                                            return;
                                        }
                                    }

                                    FlowDisposal objTempDisp = new FlowDisposal(gFrom, gTo);
                                    objTempDisp.getFormwh(this.flowpanel.Width, this.flowpanel.Height);//让Disposal中的lable得到Form的大小，防止label在移动时移出Form;
                                    int intTemp = this.lsDisposal.Count + 1;
                                    objTempDisp.dblDisposalId = Convert.ToDouble(intTemp);
                                    objTempDisp.strDisposalName = "流程" + Convert.ToString(intTemp);
                                    objTempDisp.lbDispName.Text = "流程" + Convert.ToString(intTemp);
                                    if (flag == 1)
                                    {
                                        objTempDisp.gRoutetype = FlowDisposal.eRouteType.维修流程;
                                        objTempDisp.lbDispName.BackColor = Color.FromArgb(223, 86, 57);
                                    }
                                    if (flag == 2)
                                    {
                                        objTempDisp.gRoutetype = FlowDisposal.eRouteType.重测流程;
                                        objTempDisp.lbDispName.BackColor = Color.FromArgb(163, 73, 164);
                                    }
                                    flag = 0;

                                    this.lsDisposal.Add(objTempDisp);
                                    objTempDisp.aryDisp = this.lsDisposal; //将aryDisposal传入Disposal对象.用来在删除时,remove掉Disposal列表里面对应的对象
                                    objTempDisp.aryStatusObjs = this.lsStatus;//将aryStatus传入到disposal对象，用来设置经办状态;
                                    repaint();
                                    return;
                                }
                            }
                            #endregion
                            break;
                    }//end switch
                }//end for
            }
            #endregion
        }

        /// <summary>
        /// 让状态图形始终处在画板内
        /// </summary>
        /// <param name="objStatus"></param>
        private void checkStatus(FlowStatus objStatus)
        {
            int w = this.flowpanel.Width;
            int h = this.flowpanel.Height;
            int new_x = objStatus._x;
            int new_y = objStatus._y;
            int statusW = objStatus._w;
            int statusH = objStatus._h;

            if ((objStatus._x + statusW) > w)
            {
                new_x = (int)w - statusW - 6;
            }
            if (objStatus._x < 0)
            {
                new_x = 0;
            }
            if ((objStatus._y + statusH) > h)
            {
                new_y = (int)h - statusH - 6;
            }
            if (objStatus._y < 0)
            {
                new_y = 0;
            }
            objStatus._x = new_x;
            objStatus._y = new_y;
        }

        private void updateLocation(MouseEventArgs e, FlowStatus objStatus)
        {
            objStatus.setLocation(e.X, e.Y);
            this.checkStatus(objStatus);//节点是否在画板内。
            int PosX = objStatus._x + Convert.ToInt32(objStatus._w / 2);
            int PosY = objStatus._y + Convert.ToInt32(objStatus._h / 2);
            string dblStatusId = objStatus.dblStatusId;

            foreach (FlowDisposal objDispTemp in lsDisposal)
            {
                string dblFromId = objDispTemp.dblPreStatusId;
                string dblToId = objDispTemp.dblCurStatusId;
                if (dblStatusId == dblFromId)
                {
                    objDispTemp.x1 = PosX;
                    objDispTemp.y1 = PosY;
                }
                if (dblStatusId == dblToId)
                {
                    objDispTemp.x2 = PosX;
                    objDispTemp.y2 = PosY;
                }
            }
        }

        /// <summary>
        /// 通过鼠标点的位置找到包含该点的状态对象
        /// </summary>
        /// <param name="mouseX"></param>
        /// <param name="mouseY"></param>
        /// <returns></returns>
        private FlowStatus SelectStatus(int mouseX, int mouseY)
        {
            FlowStatus objStatus = null;
            int statusSize = lsStatus.Count;
            if (statusSize == 0) return null;
            foreach (FlowStatus objTemp in lsStatus.Values)
            {
                if (objTemp.contains(mouseX, mouseY))
                {
                    objStatus = objTemp;
                    return objStatus;
                }
            }
            return objStatus;
        }
        /// <summary>
        /// 将拓扑图生成xml字符串
        /// </summary>
        /// <returns></returns>
        private string objToXml(out DataTable odbtable)
        {
            odbtable = new DataTable("atti");
            odbtable.Columns.Add("craftId", System.Type.GetType("System.String"));
            odbtable.Columns.Add("nextcraftId", System.Type.GetType("System.String"));
            odbtable.Columns.Add("stationflag", System.Type.GetType("System.Int16"));
            odbtable.Columns.Add("routedesc", System.Type.GetType("System.String"));
            odbtable.Columns.Add("seq", System.Type.GetType("System.Int16"));
            Dictionary<string, string> routedesc = new Dictionary<string, string>();//表示入口站还是出口站
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml("<?xml version=\"1.0\" encoding=\"gb2312\"?><root></root>");//根节点			
                XmlNode root = doc.DocumentElement;
                XmlElement elemMap = doc.CreateElement("FlowMap");//拓扑图节点
                XmlAttribute mapAttr = doc.CreateAttribute("ID");//拓扑图编号
                mapAttr.Value = "1";
                elemMap.Attributes.Append(mapAttr);

                XmlElement elemStatusList = doc.CreateElement("StatusList");//状态对象列表节点	
                elemMap.AppendChild(elemStatusList);
                #region FlowStatus
                foreach (FlowStatus itemStatus in this.lsStatus.Values)
                {
                    if (itemStatus.blDisplay)//状态对象没有被删除
                    {
                        XmlElement elemStatus = doc.CreateElement("Status");//状态对象节点
                        XmlAttribute StatusAttr = doc.CreateAttribute("statusId");//状态对象编号
                        StatusAttr.Value = Convert.ToString(itemStatus.dblStatusId);
                        elemStatus.Attributes.Append(StatusAttr);
                        //状态对象的其余属性用xml节点来表示

                        XmlElement elemstatusTypeId = doc.CreateElement("statusTypeId");
                        elemstatusTypeId.InnerText = Convert.ToString(itemStatus.intStatusTypeId);
                        elemStatus.AppendChild(elemstatusTypeId);

                        if (itemStatus.intStatusTypeId == 0)
                            routedesc.Add(itemStatus.dblStatusId, "IN");
                        else if (itemStatus.intStatusTypeId == 1)
                            routedesc.Add(itemStatus.dblStatusId, "OUT");
                        else
                            routedesc.Add(itemStatus.dblStatusId, "NA");
                        XmlElement elemstatusName = doc.CreateElement("statusName");
                        elemstatusName.InnerText = itemStatus.strStatusName;
                        elemStatus.AppendChild(elemstatusName);

                        XmlElement elemstatusX = doc.CreateElement("statusX");
                        elemstatusX.InnerText = Convert.ToString(itemStatus._x);
                        elemStatus.AppendChild(elemstatusX);

                        XmlElement elemstatusY = doc.CreateElement("statusY");
                        elemstatusY.InnerText = Convert.ToString(itemStatus._y);
                        elemStatus.AppendChild(elemstatusY);

                        XmlElement elemstatusJoinRule = doc.CreateElement("statusJoinRule");
                        elemstatusJoinRule.InnerText = Convert.ToString(itemStatus.strJoinRule);
                        elemStatus.AppendChild(elemstatusJoinRule);

                        XmlElement elemstatusInterfixField = doc.CreateElement("statusInterfixField");
                        elemstatusInterfixField.InnerText = Convert.ToString(itemStatus.strInterfixField);
                        elemStatus.AppendChild(elemstatusInterfixField);

                        XmlElement elemsstatusComExtend = doc.CreateElement("statusComExtend");
                        elemsstatusComExtend.InnerText = itemStatus.strComExtend;
                        elemStatus.AppendChild(elemsstatusComExtend);

                        XmlElement elemstatusChildFlowType = doc.CreateElement("statusChildFlowType");
                        elemstatusChildFlowType.InnerText = itemStatus.strChildFlowType;
                        elemStatus.AppendChild(elemstatusChildFlowType);

                        XmlElement elemstatusChildFlowHint = doc.CreateElement("statusChildFlowHint");
                        elemstatusChildFlowHint.InnerText = itemStatus.strChildFlowHint;
                        elemStatus.AppendChild(elemstatusChildFlowHint);

                        XmlElement elemstatusChildFlow = doc.CreateElement("statusChildFlow");
                        elemstatusChildFlow.InnerText = itemStatus.strChildFlow;
                        elemStatus.AppendChild(elemstatusChildFlow);

                        XmlElement elemstatusChangeFlowName = doc.CreateElement("statusChangeFlowName");
                        elemstatusChangeFlowName.InnerText = itemStatus.strChangeFlowName;
                        elemStatus.AppendChild(elemstatusChangeFlowName);

                        XmlElement elemstatusChangeStatusName = doc.CreateElement("statusChangeStatusName");
                        elemstatusChangeStatusName.InnerText = itemStatus.strChangeStatusName;
                        elemStatus.AppendChild(elemstatusChangeStatusName);

                        XmlElement elemstatusAutoFieldList = doc.CreateElement("statusAutoFieldList");
                        elemstatusAutoFieldList.InnerText = itemStatus.strAutoFieldList;
                        elemStatus.AppendChild(elemstatusAutoFieldList);

                        XmlElement elemstatusAutoRule = doc.CreateElement("statusAutoRule");
                        elemstatusAutoRule.InnerText = itemStatus.strAutoRule;
                        elemStatus.AppendChild(elemstatusAutoRule);

                        XmlElement elemstatusAutoFieldName = doc.CreateElement("statusAutoFieldName");
                        elemstatusAutoFieldName.InnerText = itemStatus.strAutoFieldName;
                        elemStatus.AppendChild(elemstatusAutoFieldName);

                        XmlElement elemstatusJoinRule2 = doc.CreateElement("statusJoinRule2");
                        elemstatusJoinRule2.InnerText = itemStatus.strJoinRule2;
                        elemStatus.AppendChild(elemstatusJoinRule2);

                        elemStatusList.AppendChild(elemStatus);//将状态对象加入到状态对象列表中
                    }
                }
                #endregion
                XmlElement elemDisposalList = doc.CreateElement("DisposalList");//处理对象列表节点
                #region FlowDisposal
                elemMap.AppendChild(elemDisposalList);
                int RouteSEQ = 1;
                foreach (FlowDisposal itemDisp in this.lsDisposal)
                {
                    if (itemDisp.blDisplay)//没有被删除
                    {
                        XmlElement elemDisp = doc.CreateElement("Disposal");//处理对象节点
                        XmlAttribute DispAttr = doc.CreateAttribute("disposalId");//处理对象编号
                        DispAttr.Value = Convert.ToString(itemDisp.dblDisposalId);
                        elemDisp.Attributes.Append(DispAttr);
                        //处理对象的其余属性用xml节点来表示

                        XmlElement elemdisposalName = doc.CreateElement("disposalName");
                        elemdisposalName.InnerText = itemDisp.strDisposalName;
                        elemDisp.AppendChild(elemdisposalName);

                        XmlElement elemrouteTypeName = doc.CreateElement("routeType");
                        elemrouteTypeName.InnerText = ((int)itemDisp.gRoutetype).ToString();
                        elemDisp.AppendChild(elemrouteTypeName);

                        XmlElement elemdisposalHint = doc.CreateElement("disposalHint");
                        elemdisposalHint.InnerText = itemDisp.strDisposalHint;
                        elemDisp.AppendChild(elemdisposalHint);

                        XmlElement elemdisposalTransactStatusId = doc.CreateElement("disposalTransactStatusId");
                        elemdisposalTransactStatusId.InnerText = Convert.ToString(itemDisp.dblTransactStatusId);
                        elemDisp.AppendChild(elemdisposalTransactStatusId);

                        XmlElement elemdisposalGroupLimit = doc.CreateElement("disposalGroupLimit");
                        elemdisposalGroupLimit.InnerText = Convert.ToString(itemDisp.intGroupLimit);
                        elemDisp.AppendChild(elemdisposalGroupLimit);

                        XmlElement elemdisposalCurStatusId = doc.CreateElement("disposalCurStatusId");
                        elemdisposalCurStatusId.InnerText = Convert.ToString(itemDisp.dblCurStatusId);
                        elemDisp.AppendChild(elemdisposalCurStatusId);

                        XmlElement elemdisposalPreStatusId = doc.CreateElement("disposalPreStatusId");
                        elemdisposalPreStatusId.InnerText = Convert.ToString(itemDisp.dblPreStatusId);
                        elemDisp.AppendChild(elemdisposalPreStatusId);

                        XmlElement elemdisposalX = doc.CreateElement("disposalX");
                        elemdisposalX.InnerText = Convert.ToString(itemDisp.intCurX);
                        elemDisp.AppendChild(elemdisposalX);

                        XmlElement elemdisposalY = doc.CreateElement("disposalY");
                        elemdisposalY.InnerText = Convert.ToString(itemDisp.intCurY);
                        elemDisp.AppendChild(elemdisposalY);

                        XmlElement elemX1 = doc.CreateElement("X1");
                        elemX1.InnerText = Convert.ToString(itemDisp.x1);
                        elemDisp.AppendChild(elemX1);

                        XmlElement elemY1 = doc.CreateElement("Y1");
                        elemY1.InnerText = Convert.ToString(itemDisp.y1);
                        elemDisp.AppendChild(elemY1);

                        XmlElement elemX2 = doc.CreateElement("X2");
                        elemX2.InnerText = Convert.ToString(itemDisp.x2);
                        elemDisp.AppendChild(elemX2);

                        XmlElement elemY2 = doc.CreateElement("Y2");
                        elemY2.InnerText = Convert.ToString(itemDisp.y2);
                        elemDisp.AppendChild(elemY2);

                        XmlElement elemiWidth = doc.CreateElement("iWidth");
                        elemiWidth.InnerText = Convert.ToString(itemDisp.iWidth);
                        elemDisp.AppendChild(elemiWidth);

                        XmlElement elemiHeight = doc.CreateElement("iHeight");
                        elemiHeight.InnerText = Convert.ToString(itemDisp.iHeight);
                        elemDisp.AppendChild(elemiHeight);


                        elemDisposalList.AppendChild(elemDisp);

                        if (!(itemDisp.dblCurStatusId == "NA" && (itemDisp.gRoutetype == FlowDisposal.eRouteType.维修流程 ? "NA" : routedesc[itemDisp.dblPreStatusId]) == "NA"))
                        {
                            odbtable.Rows.Add(
                                itemDisp.dblPreStatusId,
                                itemDisp.dblCurStatusId,
                                (int)itemDisp.gRoutetype,
                                itemDisp.gRoutetype == FlowDisposal.eRouteType.维修流程 ? "NA" : routedesc[itemDisp.dblPreStatusId],
                                // itemDisp.dblDisposalId);
                               RouteSEQ++);
                        }

                        if (routedesc[itemDisp.dblCurStatusId] != "NA")
                        {
                            odbtable.Rows.Add(
                                itemDisp.dblCurStatusId,
                                "NA",
                                (int)itemDisp.gRoutetype,
                                routedesc[itemDisp.dblCurStatusId],
                                // itemDisp.dblDisposalId);
                                 RouteSEQ++);
                        }

                    }
                }
                #endregion
                root.AppendChild(elemMap);
                //doc.Save("d:\\flowchart.xml");
                string xmlpath = System.AppDomain.CurrentDomain.BaseDirectory + "XmlDir/" + this.gRoutegroupId + ".xml";
                if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "XmlDir"))
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "XmlDir");
                doc.Save(xmlpath);
                return doc.OuterXml;
            }
            catch (Exception e)
            {

                MessageBoxEx.Show(e.Message);
                return e.Message;
            }
        }
        private void xmlToObj(string strXml)//将xml字符转换成拓扑图
        {
            this.lsStatus.Clear();
            this.lsDisposal.Clear();
            this.flowpanel.Controls.Clear();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(strXml);//根节点
                XmlNodeList eMapList = doc.GetElementsByTagName("FlowMap");
                XmlNode eMapNode = eMapList[0];//拓扑图对象
                double tmpFlowId = Convert.ToDouble(eMapNode.Attributes["ID"].Value);//拓扑图编号

                XmlNodeList eStatusList = doc.GetElementsByTagName("Status");

                for (int i = 0; i < eStatusList.Count; i++)
                {
                    FlowStatus objTemp = new FlowStatus(0, 0, this.intStatusW, this.intStautsH);

                    XmlNode eStatus = eStatusList[i];//状态对象
                    objTemp.dblStatusId = eStatus.Attributes["statusId"].Value;//状态编号

                    if (eStatus.HasChildNodes)
                    {
                        for (int j = 0; j < eStatus.ChildNodes.Count; j++)
                        {
                            if (eStatus.ChildNodes[j].Name.Equals("statusTypeId"))
                            {
                                objTemp.intStatusTypeId = Convert.ToInt32(eStatus.ChildNodes[j].InnerText);
                            }
                            if (eStatus.ChildNodes[j].Name.Equals("statusName"))
                            {
                                objTemp.strStatusName = eStatus.ChildNodes[j].InnerText;
                            }
                            if (eStatus.ChildNodes[j].Name.Equals("statusX"))
                            {
                                objTemp._x = Convert.ToInt32(eStatus.ChildNodes[j].InnerText);
                            }
                            if (eStatus.ChildNodes[j].Name.Equals("statusY"))
                            {
                                objTemp._y = Convert.ToInt32(eStatus.ChildNodes[j].InnerText);
                            }
                            if (eStatus.ChildNodes[j].Name.Equals("statusJoinRule"))
                            {
                                objTemp.strJoinRule = eStatus.ChildNodes[j].InnerText;
                            }
                            if (eStatus.ChildNodes[j].Name.Equals("statusInterfixField"))
                            {
                                objTemp.strInterfixField = eStatus.ChildNodes[j].InnerText;
                            }
                            if (eStatus.ChildNodes[j].Name.Equals("statusComExtend"))
                            {
                                objTemp.strComExtend = eStatus.ChildNodes[j].InnerText;
                            }

                            if (eStatus.ChildNodes[j].Name.Equals("statusChildFlowType"))
                            {
                                objTemp.strChildFlowType = eStatus.ChildNodes[j].InnerText;
                            }

                            if (eStatus.ChildNodes[j].Name.Equals("statusChildFlowHint"))
                            {
                                objTemp.strChildFlowHint = eStatus.ChildNodes[j].InnerText;
                            }

                            if (eStatus.ChildNodes[j].Name.Equals("statusChildFlow"))
                            {
                                objTemp.strChildFlow = eStatus.ChildNodes[j].InnerText;
                            }

                            if (eStatus.ChildNodes[j].Name.Equals("statusChangeFlowName"))
                            {
                                objTemp.strChangeFlowName = eStatus.ChildNodes[j].InnerText;
                            }
                            if (eStatus.ChildNodes[j].Name.Equals("statusChangeStatusName"))
                            {
                                objTemp.strChangeStatusName = eStatus.ChildNodes[j].InnerText;
                            }
                            if (eStatus.ChildNodes[j].Name.Equals("statusAutoFieldList"))
                            {
                                objTemp.strAutoFieldList = eStatus.ChildNodes[j].InnerText;
                            }

                            if (eStatus.ChildNodes[j].Name.Equals("statusAutoRule"))
                            {
                                objTemp.strAutoRule = eStatus.ChildNodes[j].InnerText;
                            }

                            if (eStatus.ChildNodes[j].Name.Equals("statusAutoFieldName"))
                            {
                                objTemp.strAutoFieldName = eStatus.ChildNodes[j].InnerText;
                            }

                            if (eStatus.ChildNodes[j].Name.Equals("statusJoinRule2"))
                            {
                                objTemp.strJoinRule2 = eStatus.ChildNodes[j].InnerText;
                            }
                        }//end for			
                    }//end if
                    this.lsStatus.Add(objTemp.dblStatusId, objTemp);
                }//end for

                XmlNodeList eDispList = doc.GetElementsByTagName("Disposal");

                for (int i = 0; i < eDispList.Count; i++)
                {
                    FlowDisposal tmpDisp = new FlowDisposal();
                    XmlNode eDisposal = eDispList[i];//状态对象
                    tmpDisp.dblDisposalId = Convert.ToDouble(eDisposal.Attributes["disposalId"].Value);//状态编号

                    if (eDisposal.HasChildNodes)
                    {
                        for (int j = 0; j < eDisposal.ChildNodes.Count; j++)
                        {
                            if (eDisposal.ChildNodes[j].Name.Equals("disposalName"))
                            {
                                tmpDisp.strDisposalName = eDisposal.ChildNodes[j].InnerText;
                            }
                            if (eDisposal.ChildNodes[j].Name.Equals("disposalHint"))
                            {
                                tmpDisp.strDisposalHint = eDisposal.ChildNodes[j].InnerText;
                            }

                            if (eDisposal.ChildNodes[j].Name.Equals("routeType"))
                            {
                                tmpDisp.gRoutetype = Convert.ToInt16(eDisposal.ChildNodes[j].InnerText) == 1 ?
                                    FlowDisposal.eRouteType.维修流程 : Convert.ToInt16(eDisposal.ChildNodes[j].InnerText) == 2 ?
                                    FlowDisposal.eRouteType.重测流程 : FlowDisposal.eRouteType.正常流程;
                            }

                            if (eDisposal.ChildNodes[j].Name.Equals("disposalTransactStatusId"))
                            {
                                tmpDisp.dblTransactStatusId = Convert.ToDouble(eDisposal.ChildNodes[j].InnerText);
                            }

                            if (eDisposal.ChildNodes[j].Name.Equals("disposalGroupLimit"))
                            {
                                tmpDisp.intGroupLimit = Convert.ToInt32(eDisposal.ChildNodes[j].InnerText);
                            }

                            if (eDisposal.ChildNodes[j].Name.Equals("disposalCurStatusId"))
                            {
                                tmpDisp.dblCurStatusId = eDisposal.ChildNodes[j].InnerText;
                            }
                            if (eDisposal.ChildNodes[j].Name.Equals("disposalPreStatusId"))
                            {
                                tmpDisp.dblPreStatusId = eDisposal.ChildNodes[j].InnerText;
                            }

                            if (eDisposal.ChildNodes[j].Name.Equals("disposalX"))
                            {
                                tmpDisp.intCurX = Convert.ToInt32(eDisposal.ChildNodes[j].InnerText);
                            }

                            if (eDisposal.ChildNodes[j].Name.Equals("disposalY"))
                            {
                                tmpDisp.intCurY = Convert.ToInt32(eDisposal.ChildNodes[j].InnerText);
                            }
                            if (eDisposal.ChildNodes[j].Name.Equals("X1"))
                            {
                                tmpDisp.x1 = Convert.ToInt32(eDisposal.ChildNodes[j].InnerText);
                            }
                            if (eDisposal.ChildNodes[j].Name.Equals("Y1"))
                            {
                                tmpDisp.y1 = Convert.ToInt32(eDisposal.ChildNodes[j].InnerText);
                            }
                            if (eDisposal.ChildNodes[j].Name.Equals("X2"))
                            {
                                tmpDisp.x2 = Convert.ToInt32(eDisposal.ChildNodes[j].InnerText);
                            }
                            if (eDisposal.ChildNodes[j].Name.Equals("Y2"))
                            {
                                tmpDisp.y2 = Convert.ToInt32(eDisposal.ChildNodes[j].InnerText);
                            }
                            if (eDisposal.ChildNodes[j].Name.Equals("iWidth"))
                            {
                                tmpDisp.iWidth = Convert.ToInt32(eDisposal.ChildNodes[j].InnerText);
                            }
                            if (eDisposal.ChildNodes[j].Name.Equals("iHeight"))
                            {
                                tmpDisp.iHeight = Convert.ToInt32(eDisposal.ChildNodes[j].InnerText);
                            }

                        }//end for
                    }//end if
                    tmpDisp.getFormwh(this.flowpanel.Width, this.flowpanel.Height);
                    tmpDisp.setLabelProp();
                    this.lsDisposal.Add(tmpDisp);
                    tmpDisp.aryDisp = this.lsDisposal;
                    tmpDisp.aryStatusObjs = this.lsStatus;

                }//end for
                this.repaint();

            }//end try
            catch
            {
                string strError = "格式错误,转换出错!";
                MessageBox.Show(strError);
            }
        }
        private Dictionary<string, EntityRoute> myRouteModel = new Dictionary<string, EntityRoute>();
        public class EntityRoute
        {
            public string craftId { get; set; }
            public string nextcraftId { get; set; }
            public int stationflag { get; set; }
            public string routedesc { get; set; }
            public int seq { get; set; }
        }
        #endregion
        #region 绘图事件
        private void myMouseDown(object sender, MouseEventArgs ex)
        {
            int intClickCount = ex.Clicks;
            if (ex.Button == MouseButtons.Left)
            {
                if (intClickCount == 1)
                {
                    this.drawMouseObjs(ex, 1);
                }
                #region 鼠标左键双击
                if (intClickCount == 2)
                {
                    this.gStatus = this.SelectStatus(ex.X, ex.Y);//选中了某状态对象 
                    if (gStatus != null)
                        this.LoadRrouteParameterData(gStatus.dblStatusId.Split('_')[0]);
                }
                #endregion
            }

            if (ex.Button == MouseButtons.Right)//鼠标右键
            {
                this.gStatus = this.SelectStatus(ex.X, ex.Y);//选中了某状态对象 
                if (gStatus != null)
                {
                    this.showStatusMenu(); //显示设置当前状态对象的菜单
                }
                else
                {
                    this.flowpanel.ContextMenu = null;
                    showMenu(ex);//显示系统菜单
                }
            }
        }
        /// <summary>
        /// 显示增加时的右键菜单
        /// </summary>
        /// <param name="ex"></param>
        public void showMenu(MouseEventArgs ex)
        {
            this.intMouseX = ex.X;
            this.intMouseY = ex.Y;
            this.menuAdd = new System.Windows.Forms.ContextMenu();
            this.menuSaveXml = new System.Windows.Forms.MenuItem();
            this.menuLoadXml = new System.Windows.Forms.MenuItem();

            this.menuAdd.MenuItems.AddRange(
                new System.Windows.Forms.MenuItem[] { this.menuSaveXml, this.menuLoadXml });

            this.menuSaveXml.Index = 0;
            this.menuSaveXml.Text = "保存生产流程";
            this.menuSaveXml.Enabled = false;
            this.menuSaveXml.Click += new EventHandler(menuXml_Click);

            this.menuLoadXml.Index = 1;
            this.menuLoadXml.Text = "加载生产流程";
            this.menuLoadXml.Enabled = false;
            this.menuLoadXml.Click += new EventHandler(menuLoadXml_Click);


            this.flowpanel.ContextMenu = this.menuAdd;
        }
        private void menuLoadXml_Click(object sender, EventArgs e)
        {
            //xmlToObj("d:\\flowchart.xml");

        }
        private void showStatusMenu()
        {
            this.menuDelSet = new System.Windows.Forms.ContextMenu();
            this.menuDelStatus = new System.Windows.Forms.MenuItem();
            this.menuSetStatus = new System.Windows.Forms.MenuItem();
            this.menuSetRouteStart = new System.Windows.Forms.MenuItem();
            this.menuSetRouteEnd = new System.Windows.Forms.MenuItem();
            this.menuClearRouteSet = new System.Windows.Forms.MenuItem();

            this.menuSetStatus.Text = "流程状态设置";

            this.menuSetRouteStart.Text = "流程开始";
            this.menuSetRouteStart.Click += new EventHandler(menuSetRouteStart_Click);

            this.menuSetRouteEnd.Text = "流程结束";
            this.menuSetRouteEnd.Click += new EventHandler(menuSetRouteEnd_Click);

            this.menuClearRouteSet.Text = "清除流程状态";
            this.menuClearRouteSet.Click += new EventHandler(menuClearRouteSet_Click);

            this.menuSetStatus.MenuItems.AddRange(
                new MenuItem[] { this.menuSetRouteStart, this.menuSetRouteEnd, this.menuClearRouteSet });

            this.menuDelStatus.Text = "删除";
            this.menuDelStatus.Click += new System.EventHandler(this.menuDelStatus_Click);

            this.menuDelSet.MenuItems.Add(menuSetStatus);
            this.menuDelSet.MenuItems.Add(menuDelStatus);
            this.flowpanel.ContextMenu = this.menuDelSet;
        }
        private void menuClearRouteSet_Click(object sender, EventArgs e)
        {
            if (this.gStatus != null)
            {
                gStatus.intStatusTypeId = 2;
                this.gStatus = null;
                this.repaint();
            }
        }
        private void menuSetRouteEnd_Click(object sender, EventArgs e)
        {

            if (this.gStatus != null)
            {
                foreach (FlowStatus item in this.lsStatus.Values)
                {
                    if (item.intStatusTypeId == 1)
                    {
                        MessageBoxEx.Show("已经存在一个结束流程..");
                        return;
                    }
                }

                foreach (FlowDisposal fd in this.lsDisposal)
                {
                    if (fd.dblCurStatusId == gStatus.dblStatusId)
                    {
                        //if (fd.gRoutetype == FlowDisposal.eRouteType.维修流程)
                        //{
                        //    MessageBoxEx.Show("结束节点不能作为维修节点");
                        //    return;
                        //}
                        fd.gRoutetype = FlowDisposal.eRouteType.正常流程;
                        fd.lbDispName.BackColor = Color.FromArgb(39, 50, 241);
                    }
                    if (fd.dblPreStatusId == gStatus.dblStatusId)
                    {
                        fd.gRoutetype = FlowDisposal.eRouteType.维修流程;
                        fd.lbDispName.BackColor = Color.FromArgb(223, 86, 57);
                    }
                }
                gStatus.intStatusTypeId = 1;
                this.gStatus = null;
                this.repaint();
            }
        }
        private void menuSetRouteStart_Click(object sender, EventArgs e)
        {
            if (this.gStatus != null)
            {
                foreach (FlowStatus item in this.lsStatus.Values)
                {
                    if (item.intStatusTypeId == 0)
                    {
                        MessageBoxEx.Show("已经存在一个开始流程..");
                        return;
                    }
                }
                gStatus.intStatusTypeId = 0;
                this.gStatus = null;
                this.repaint();
            }
        }
        private void menuCommon_Click(object sender, System.EventArgs e)
        {
            bool flag = false;
            foreach (string item in this.lsCraftItemInfo.Keys)
            {
                if (item == this.listView1.FocusedItem.Name)
                    flag = true;
            }
            if (!flag)
                this.SaveCraftInfo(this.listView1.FocusedItem.Name,
                    this.listView1.FocusedItem.Text);
            if (flag)
            {
                MessageBoxEx.Show("工艺已经存在流程中,不能再次添加");
                return;
            }
            this._estatustypeid = eStatusTypeId.普通;
            addStatus(this.listView1.FocusedItem.Name, this.listView1.FocusedItem.Text);
        }
        /// <summary>
        /// 删除一个状态(status)对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuDelStatus_Click(object sender, System.EventArgs e)
        {
            if (this.gStatus != null)
            {
                if (gStatus.intStatusTypeId == 0)
                {
                    MessageBox.Show("开始状态不能删除!");
                    return;
                }

                if (gStatus.intStatusTypeId == 1)
                {
                    MessageBox.Show("结束状态不能删除!");
                    return;
                }

                if (MessageBoxEx.Show(string.Format("是否确定删除:[{0}]节点?\n\n确认[Yes] 取消[No]",
                    gStatus.strStatusName), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.No)
                    return;

                this.RemoveCraftInfo(gStatus.dblStatusId.Split('_')[0]);
                string tempStatusId = gStatus.dblStatusId;

                int j = lsDisposal.Count;
                for (int i = 0; i < j; i++)
                {
                    FlowDisposal objDisp = lsDisposal[i];
                    if (objDisp.dblCurStatusId == tempStatusId || objDisp.dblPreStatusId == tempStatusId)
                    {
                        objDisp.clearProperty();
                    }
                }
                this.gStatus.clearProperty();
                this.gStatus = null;
                this.repaint();
            }
        }
        private void myMouseUp(object sender, MouseEventArgs ex)
        {
            if (ex.Button == MouseButtons.Left)
            {
                this.drawMouseObjs(ex, 3);
            }
        }
        private void myMouseMove(object sender, MouseEventArgs ex)
        {
            if (ex.Button == MouseButtons.Left)
            {
                this.drawMouseObjs(ex, 2);
            }
        }
        public void myKeyUp(object sender, KeyEventArgs ex)//键盘中ctrl键被松开
        {
            if (ex.Control == true)
            {
                int intCount = this.lsStatus.Count;
                if (intCount == 0)
                {
                    return;
                }
                else
                {
                    foreach (FlowStatus objTemp in lsStatus.Values)
                    {
                        objTemp.isMove = 1;
                    }
                }

            }
            this.blCtrlDown = false;
        }
        public void myKeyDown(object sender, KeyEventArgs ex)
        {
            if (ex.Control == true)//键盘中ctrl键被按下
            {
                int intCount = this.lsStatus.Count;
                if (intCount == 0)
                {
                    return;
                }
                else
                {
                    foreach (FlowStatus objTemp in lsStatus.Values)
                    {
                        objTemp.isMove = 0;
                    }

                }
                this.blCtrlDown = true;
            }
        }

        private void flowpanel_Paint(object sender, PaintEventArgs e)
        {
            this.repaint();
        }
        public void menuXml_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveRouteToDb();
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }
        #endregion

        #region 公共
        /// <summary>
        /// 获取项目参数实体集合
        /// </summary>
        /// <param name="craftId"></param>
        /// <returns></returns>
        private List<Dictionary<string, object>> GetRouteCraftItemParameterEntitys(string craftId)
        {
            List<Dictionary<string, object>> routpara = new List<Dictionary<string, object>>();
            Dictionary<string, object> dic = null;
            for (int i = 0; i < lsCraftItemInfo[craftId].craftItemParametList.Rows.Count; i++)
            {
                if (lsCraftItemInfo[craftId].craftItemParametList.Rows[i].RowState != DataRowState.Deleted)
                {
                    dic = new Dictionary<string, object>();
                    dic.Add("CRAFTID", lsCraftItemInfo[craftId].craftItemParametList.Rows[i]["craftId"].ToString());
                    dic.Add("CRAFTITEM", int.Parse(lsCraftItemInfo[craftId].craftItemParametList.Rows[i]["craftItem"].ToString()));
                    dic.Add("CRAFTPARAMETERDES", lsCraftItemInfo[craftId].craftItemParametList.Rows[i]["craftparameterdes"].ToString());
                    dic.Add("UPPERLIMIT", lsCraftItemInfo[craftId].craftItemParametList.Rows[i]["upperlimit"].ToString());
                    dic.Add("LOWERLIMIT", lsCraftItemInfo[craftId].craftItemParametList.Rows[i]["lowerlimit"].ToString());
                    dic.Add("OTHER", lsCraftItemInfo[craftId].craftItemParametList.Rows[i]["other"].ToString());
                    dic.Add("ROUTGROUPID", this.gRoutegroupId);
                    routpara.Add(dic);

                    //routpara.Add(new WebServices.tRouteInfo.tRoutCraftparameter()
                    //{
                    //    craftId = lsCraftItemInfo[craftId].craftItemParametList.Rows[i]["craftId"].ToString(),
                    //    craftItem = int.Parse(lsCraftItemInfo[craftId].craftItemParametList.Rows[i]["craftItem"].ToString()),
                    //    craftparameterdes = lsCraftItemInfo[craftId].craftItemParametList.Rows[i]["craftparameterdes"].ToString(),
                    //    upperlimit = lsCraftItemInfo[craftId].craftItemParametList.Rows[i]["upperlimit"].ToString(),
                    //    lowerlimit = lsCraftItemInfo[craftId].craftItemParametList.Rows[i]["lowerlimit"].ToString(),
                    //    other = lsCraftItemInfo[craftId].craftItemParametList.Rows[i]["other"].ToString(),
                    //    routgroupId = this.gRoutegroupId
                    //});
                }
            }
            return routpara;
        }

        //private List<Dictionary<string,object>> GetRouteCraftItemParameterEntitys1(string craftId)
        //{

        //    List<Dictionary<string, object>> routpara = new List<Dictionary<string, object>>();
        //    Dictionary<string, object> dic = null;
        //    for (int i = 0; i < lsCraftItemInfo[craftId].craftItemParametList.Rows.Count; i++)
        //    {
        //        if (lsCraftItemInfo[craftId].craftItemParametList.Rows[i].RowState != DataRowState.Deleted)
        //        {
        //            dic = new Dictionary<string, object>();
        //            dic.Add("CRAFTID", lsCraftItemInfo[craftId].craftItemParametList.Rows[i]["craftId"].ToString());
        //            dic.Add("CRAFTITEM", int.Parse(lsCraftItemInfo[craftId].craftItemParametList.Rows[i]["craftItem"].ToString()));
        //            dic.Add("CRAFTPARAMETERDES", lsCraftItemInfo[craftId].craftItemParametList.Rows[i]["craftparameterdes"].ToString());
        //            dic.Add("UPPERLIMIT", lsCraftItemInfo[craftId].craftItemParametList.Rows[i]["upperlimit"].ToString());
        //            dic.Add("LOWERLIMIT", lsCraftItemInfo[craftId].craftItemParametList.Rows[i]["lowerlimit"].ToString());
        //            dic.Add("OTHER", lsCraftItemInfo[craftId].craftItemParametList.Rows[i]["other"].ToString());
        //            dic.Add("ROUTGROUPID", this.gRoutegroupId);
        //            routpara.Add(dic);
        //            //routpara.Add(new WebServices.tRouteInfo.tRoutCraftparameter()
        //            //{
        //            //    craftId = lsCraftItemInfo[craftId].craftItemParametList.Rows[i]["craftId"].ToString(),
        //            //    craftItem = int.Parse(lsCraftItemInfo[craftId].craftItemParametList.Rows[i]["craftItem"].ToString()),
        //            //    craftparameterdes = lsCraftItemInfo[craftId].craftItemParametList.Rows[i]["craftparameterdes"].ToString(),
        //            //    upperlimit = lsCraftItemInfo[craftId].craftItemParametList.Rows[i]["upperlimit"].ToString(),
        //            //    lowerlimit = lsCraftItemInfo[craftId].craftItemParametList.Rows[i]["lowerlimit"].ToString(),
        //            //    other = lsCraftItemInfo[craftId].craftItemParametList.Rows[i]["other"].ToString(),
        //            //    routgroupId = this.gRoutegroupId
        //            //});
        //        }
        //    }
        //    return routpara;
        //}

        private void CreateRouteGroup_Load(object sender, EventArgs e)
        {
            #region 添加应用程序
            if (this.mFrm.gUserInfo.rolecaption == "系统开发员")
            {
                IList<IDictionary<string, object>> lsfunls = new List<IDictionary<string, object>>();
                FrmBLL.publicfuntion.GetFromCtls(this, ref lsfunls);
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("PROGID", this.Name);
                dic.Add("PROGNAME", this.Text);
                dic.Add("PROGDESC", this.Text);
                FrmBLL.publicfuntion.AddProgInfo(dic, lsfunls);
            }
            #endregion
            #region 初始化变量
            this.gRoutegroupId = string.Empty;
            this.gRoutegroupname = string.Empty;

            this.CreateDataTable();
            //this.tabStrip1.SelectNextTab();
            this.ShowWoList();
            this.tabStrip1.SelectedTabIndex = 0;
            if (!this.runstrip)
            {
                tabStrip1.SelectNextTab();
            }
            #endregion

            DataTable dt = FrmBLL.ReleaseData.arrByteToDataTable(refWebtCraftInfo.Instance.GetAllCraftInfo());
            foreach (DataRow dr in dt.Rows)
            {
                SetCraftName.Add(dr["craftId"].ToString(), dr["craftname"].ToString());
            }
        }
        /// <summary>
        /// 显示工单信息
        /// </summary>
        private void ShowWoList()
        {
            FillDataGridView(FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWoInfo.Instance.GetWoInfo(null,null,null)));
        }
        private DataTable mCraftIdTable = new DataTable("mCraftIdTable");
        private DataTable mRouteIdTable = new DataTable("mRouteIdTable");
        /// <summary>
        /// 显示工艺
        /// </summary>
        private void ShowCraftList(DataTable _dt)
        {
            this.listView1.Items.Clear();
            this.listView1.LargeImageList = this.imageList1;
            //首先获取所有的工艺

            // DataTable craft = mCraftIdTable  = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtCraftInfo.Instance.GetAllCraftItme());
            foreach (DataRow dr in _dt.DefaultView.ToTable(true, "beworkseg").Rows)
            {
                this.listView1.Groups.Add(dr["beworkseg"].ToString(), dr["beworkseg"].ToString());
            }
            foreach (DataRow dr in _dt.Rows)
            {
                ListViewItem lv = new ListViewItem();
                lv.ImageIndex = 0;
                lv.Text = dr["craftname"].ToString();
                lv.Name = dr["craftId"].ToString();
                lv.ToolTipText = dr["craftname"].ToString();
                lv.Group = this.listView1.Groups[dr["beworkseg"].ToString()];
                this.listView1.Items.Add(lv);
            }
        }
        /// <summary>
        /// 显示流程
        /// </summary>
        private void ShowRouteList(DataTable _dt)
        {
            this.listView1.Items.Clear();
            this.listView1.LargeImageList = this.imageList1;
            //首先获取所有的工艺
            //DataTable craft = mRouteIdTable = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtRouteInfo.Instance.GetAllRouteAtt());
            foreach (DataRow dr in _dt.Rows)
            {
                ListViewItem lv = new ListViewItem();
                lv.ImageIndex = 1;
                lv.Text = dr["routgroupdesc"].ToString();
                lv.Name = dr["routgroupId"].ToString();
                lv.ToolTipText = dr["routgroupdesc"].ToString();
                this.listView1.Items.Add(lv);
            }
        }

        private void SaveRouteBitmap()
        {
            Point p = new Point();
            if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "XmlDir"))
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "XmlDir");
            string xmlbitmappath = System.AppDomain.CurrentDomain.BaseDirectory + "XmlDir/" + this.gRoutegroupId + ".png";
            Bitmap bp = new Bitmap(this.flowpanel.Width, this.flowpanel.Height);
            Graphics g = Graphics.FromImage(bp);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.CopyFromScreen(p = this.flowpanel.PointToScreen(Point.Empty), Point.Empty, this.flowpanel.Size);
            bp.Save(xmlbitmappath);
        }
        private void bt_saveroute_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.gRoutegroupId))
                    throw new Exception("没有可以保存的流程");
                //保存图片
                this.SaveRouteBitmap();
                DialogResult result =
                    MessageBoxEx.Show(
                    string.Format("是否确认保存流程:\n\n流程编号[{0}]\n流程名称:[{1}]\n\n保存[Yes] 不保存[No]",
                      this.gRoutegroupId, this.gRoutegroupname),
                      "提示", MessageBoxButtons.YesNo,
                      MessageBoxIcon.Asterisk);
                if (result == DialogResult.Yes)
                {
                    Thread thread = new Thread(new ThreadStart(this.SaveRouteToDb));
                    thread.Start();
                    refwebtEditing.Instance.DeletetEditingByfunname(gRoutegroupId);
                    //this.SaveRouteToDb();
                }
                else
                    throw new Exception("取消保存");
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }


        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (!this.isCraftOrRoute)
                {
                    #region 添加工艺
                    if (this.mRoutegrouptype == eRouteGroupType.UpdateRoute)
                    {
                        if (MessageBoxEx.Show(string.Format("是否确认修改当前流程:[{0}] ?\n\n确认[Yes] 退出[No]",
                             this.gRoutegroupId),
                             "提示",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Asterisk) != DialogResult.Yes)
                            return;
                        this.mRoutegrouptype = eRouteGroupType.NewRoute;
                    }
                    if (this.mRoutegrouptype == eRouteGroupType.NullRoute)
                    {
                        RoutegroupAtt rga = new RoutegroupAtt(this);
                        if (rga.ShowDialog() != DialogResult.OK)
                            return;
                    }
                    this.lb_routegroupId.Text = this.gRoutegroupId;
                    this.lb_routegroupname.Text = this.gRoutegroupname;
                    this.menuCommon_Click(null, null);
                    this.mRoutegrouptype = eRouteGroupType.NewRoute;
                    #endregion
                }
                else
                {
                    #region 加载流程
                    if (!string.IsNullOrEmpty(this.gRoutegroupId))
                    {
                        this.SaveRouteBitmap();

                        DialogResult result = MessageBoxEx.Show(
                            string.Format("是否确认保存流程:\n\n流程编号[{0}]\n流程名称:[{1}]\n\n保存[Yes] 不保存[No] 取消[Cancel]",
                            this.gRoutegroupId, this.gRoutegroupname),
                            "提示",
                            MessageBoxButtons.YesNoCancel,
                            MessageBoxIcon.Asterisk);
                        if (result == DialogResult.Cancel)
                            return;
                        if (result == DialogResult.Yes)
                        {
                            Thread thread = new Thread(new ThreadStart(this.SaveRouteToDb));
                            thread.Start();
                            //this.SaveRouteToDb();
                        }
                    }

                    string err = FrmBLL.publicfuntion.ChktEditing(this.listView1.FocusedItem.Name, this.strFnumName, this.mFrm.gUserInfo.userId, this.mFrm.gUserInfo.username);

                    if (err != "OK")
                    {
                        if (err.IndexOf("ERROR") != -1)
                        {
                            this.mFrm.ShowPrgMsg(err, MainParent.MsgType.Error);
                            return;
                        }
                        else
                        {
                            MessageBoxEx.Show(string.Format("用户:[{0}]/[{1}]--{2}正在编辑中,您现在不能进行修改!!",
     err.Split(':')[1].Split(',')[0], err.Split(':')[1].Split(',')[1], err.Split(':')[1].Split(',')[2]), "提示!!");
                            return;
                        }
                    }
                    LoadXmlRoute(this.listView1.FocusedItem.Name);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        /// <summary>
        /// 保存生产流程
        /// </summary>
        private void SaveRouteToDb()
        {
            try
            {
                bool endroute = false;
                bool startroute = false;
                if (string.IsNullOrEmpty(this.gRoutegroupId))
                    throw new Exception("没有流程编号,不能保存!");

                foreach (FlowStatus fs in this.lsStatus.Values)
                {
                    if (fs.intStatusTypeId == 0)
                        startroute = true;
                    if (fs.intStatusTypeId == 1)
                        endroute = true;
                }
                if (!endroute)
                    throw new Exception("没有结束流程,不能保存");
                if (!startroute)
                    throw new Exception("没有开始流程,不能保存");

                DataTable mAtt;
                string xmlcontent = this.objToXml(out mAtt);

                #region 流程内容
                List<Dictionary<string, object>> _lsroutinfo = new List<Dictionary<string, object>>();
                Dictionary<string, object> dic = null;
                foreach (DataRow dr in mAtt.Rows)
                {
                    dic = new Dictionary<string, object>();
                    dic.Add("CRAFTID", dr["craftId"].ToString().Split('_')[0]);
                    dic.Add("NEXTROUTEID", dr["nextcraftId"].ToString().Split('_')[0]);
                    dic.Add("ROUTEDESC", dr["routedesc"].ToString());
                    dic.Add("SEQ", int.Parse(dr["seq"].ToString()));
                    dic.Add("STATION_FLAG", int.Parse(dr["stationflag"].ToString()));
                    dic.Add("ROUTGROUPID", this.gRoutegroupId);
                    dic.Add("CRAFTNAME", SetCraftName[dr["craftId"].ToString().Split('_')[0]]);
                    dic.Add("NEXTCRAFTNAME", SetCraftName[dr["nextcraftId"].ToString().Split('_')[0]]);
                    dic.Add("LsRouteCraftparameter".ToUpper(), this.GetRouteCraftItemParameterEntitys(dr["craftId"].ToString().Split('_')[0]));
                    _lsroutinfo.Add(dic);
                    //_lsroutinfo.Add(new WebServices.tRouteInfo.tRouteInfo1()
                    //{
                    //    craftId = dr["craftId"].ToString().Split('_')[0],
                    //    nextrouteId = dr["nextcraftId"].ToString().Split('_')[0],
                    //    routedesc = dr["routedesc"].ToString(),
                    //    seq = int.Parse(dr["seq"].ToString()),
                    //    station_flag = int.Parse(dr["stationflag"].ToString()),
                    //    routgroupId = this.gRoutegroupId,
                    //    CraftName=SetCraftName[dr["craftId"].ToString().Split('_')[0]],
                    //    NextCtaftName = SetCraftName[dr["nextcraftId"].ToString().Split('_')[0]],                        
                    //    LsRouteCraftparameter = this.GetRouteCraftItemParameterEntitys(dr["craftId"].ToString().Split('_')[0]).ToArray()
                    //});
                }
                #endregion

                #region XML
                //  WebServices.tRouteInfo.tRouteAtt _routeAtt = new WebServices.tRouteInfo.tRouteAtt();
                Dictionary<string, object> _routeAtt = new Dictionary<string, object>();
                _routeAtt.Add("ROUTGROUPID", this.gRoutegroupId);
                _routeAtt.Add("ROUTGROUPDESC", this.gRoutegroupname);
                _routeAtt.Add("ROUTGROUPXMLCONTENT", xmlcontent);
                _routeAtt.Add("LSROUTE", _lsroutinfo);
                //_routeAtt.routgroupId = this.gRoutegroupId;
                //_routeAtt.routgroupdesc = this.gRoutegroupname;
                //_routeAtt.routgroupxmlContent = xmlcontent;
                //_routeAtt.LsRouteInfo = _lsroutinfo.ToArray();
                string sRes = RefWebService_BLL.refWebtRouteInfo.Instance.InsertRouteAllItme(FrmBLL.ReleaseData.DictionaryToJson(_routeAtt));
                if (sRes != "OK")
                {
                    MessageBox.Show("InsertRouteAllItme Error:" + sRes,"提示",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                    return;
                }
                #endregion

                #region 保存修改记录log

                FrmBLL.publicfuntion.InserSystemLog(mFrm.gUserInfo.userId, "RouteEdit", "RouteEdit", "RouteEdit: " + this.gRoutegroupId);

                #endregion

                //this.mFrm.ShowPrgMsg("保存成功", MainParent.MsgType.Outgoing);
            

                FrmBLL.Ftp_MyFtp ftp = new FrmBLL.Ftp_MyFtp();
                ftp.PutImage(System.AppDomain.CurrentDomain.BaseDirectory + "XmlDir/" + this.gRoutegroupId + ".png");
                ftp.PutImage(System.AppDomain.CurrentDomain.BaseDirectory + "XmlDir/" + this.gRoutegroupId + ".xml");
                MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
                return;
                // throw ex;
            }
        }
        /// <summary>
        /// 加载XML文档到流程图
        /// </summary>
        /// <param name="routeId"></param>
        private void LoadXmlRoute(string routeId)
        {
            this.dtlsallcip.Rows.Clear();
            this.lsCraftItemInfo = new Dictionary<string, CraftItemInfo>();
            xmlToObj(RefWebService_BLL.refWebtRouteInfo.Instance.GetRouteAttBy(routeId));
            foreach (FlowStatus obj in this.lsStatus.Values)
            {
                this.SaveCraftInfo(obj.dblStatusId.Split('_')[0],
                    obj.strStatusName);
            }
            this.mRoutegrouptype = eRouteGroupType.UpdateRoute;
            this.gRoutegroupname = this.lb_routegroupname.Text = RefWebService_BLL.refWebtRouteInfo.Instance.GetAttRouteDesc(routeId);
            this.gRoutegroupId = this.lb_routegroupId.Text = routeId;
        }
        /// <summary>
        /// 将参数保存到内存
        /// </summary>
        /// <param name="craftname"></param>
        private void SaveCraftInfo(string craftid, string craftname)
        {
            //将参数保存到内存中
            DataTable CraftItemDt = !this.isCraftOrRoute ?
                FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtCraftInfo.Instance.GetCraftItemByCraftId(craftid)) :
                FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtRouteInfo.Instance.GetRouteCraftParameterByWoId(craftid));
            this.lsCraftItemInfo.Add(craftid, new CraftItemInfo()
            {
                woId = "NA",
                craftItemId = craftid,
                craftItemName = craftname,
                craftItemParametList = CraftItemDt,
                craftItemToal = CraftItemDt.Rows.Count
            });

            //将参数显示在datagridview中    
            dtlsallcip.Rows.Add(lsCraftItemInfo[craftid].woId,
                lsCraftItemInfo[craftid].craftItemId,
                lsCraftItemInfo[craftid].craftItemName,
                lsCraftItemInfo[craftid].craftItemToal);
            this.ShowListAllCraftItemPara(dtlsallcip);
        }
        /// <summary>
        /// 从内存中删除参数
        /// </summary>
        /// <param name="craftname"></param>
        private void RemoveCraftInfo(string craftId)
        {
            this.lsCraftItemInfo.Remove(craftId);
            dtlsallcip = new DataTable();
            CreateDataTable();
            //将参数显示在datagridview中   
            foreach (string item in this.lsCraftItemInfo.Keys)
            {
                dtlsallcip.Rows.Add(lsCraftItemInfo[item].woId,
                    lsCraftItemInfo[item].craftItemId,
                    lsCraftItemInfo[item].craftItemName,
                    lsCraftItemInfo[item].craftItemParametList.Rows.Count);
            }
            this.dgv_listallcraftitemparamet.DataSource = dtlsallcip;
        }

        /// <summary>
        /// 更新项目和项目参数
        /// </summary>
        /// <param name="craftId"></param>
        private void UpdateCraftInfo()
        {
            dtlsallcip = new DataTable();
            CreateDataTable();
            //将参数显示在datagridview中   
            foreach (string item in this.lsCraftItemInfo.Keys)
            {
                dtlsallcip.Rows.Add(lsCraftItemInfo[item].woId,
                    lsCraftItemInfo[item].craftItemId,
                    lsCraftItemInfo[item].craftItemName,
                    lsCraftItemInfo[item].craftItemParametList.Rows.Count);
            }
            this.dgv_listallcraftitemparamet.DataSource = dtlsallcip;
        }
        private void ShowListAllCraftItemPara(DataTable dt)
        {
            this.dgv_listallcraftitemparamet.Invoke(new EventHandler(delegate
            {
                this.dgv_listallcraftitemparamet.DataSource = dt;
            }));
        }
        bool runstrip = false;
        int iIsCraftOrRoute = 0;
        private void tabStrip1_SelectedTabChanged(object sender, TabStripTabChangedEventArgs e)
        {
            if (e.NewTab.Name == this.tbitem_craftlist.Name)
            {//选择工艺
                this.isCraftOrRoute = false;
                this.mCraftIdTable = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtCraftInfo.Instance.GetAllCraftItme());
                ShowCraftList(mCraftIdTable);
                this.iIsCraftOrRoute = 0;
            }
            else
            {//选择流程
                this.isCraftOrRoute = true;
                this.mRouteIdTable = FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtRouteInfo.Instance.GetAllRouteAtt());
                ShowRouteList(mRouteIdTable);
                this.iIsCraftOrRoute = 1;
            }
            runstrip = true;
        }
        private void CreateDataTable()
        {
            dtlsallcip.Columns.Add("craftwoid", System.Type.GetType("System.String"));
            dtlsallcip.Columns.Add("craftinfoid", System.Type.GetType("System.String"));
            dtlsallcip.Columns.Add("craftinfoname", System.Type.GetType("System.String"));
            dtlsallcip.Columns.Add("craftitemtotal", System.Type.GetType("System.Int32"));
        }

        /// <summary>
        /// 工艺Id和工艺名称以及对应的参数
        /// </summary>
        private class CraftItemInfo
        {
            /// <summary>
            /// 工单号
            /// </summary>
            public string woId { get; set; }
            /// <summary>
            /// 工艺Id
            /// </summary>
            public string craftItemId { get; set; }
            /// <summary>
            /// 工艺描述
            /// </summary>
            public string craftItemName { get; set; }
            /// <summary>
            /// 工艺项目参数即途程的参数
            /// </summary>
            public DataTable craftItemParametList { get; set; }
            /// <summary>
            /// 途程数量
            /// </summary>
            public int craftItemToal { get; set; }
        }
        #endregion

        private void dgv_listallcraftitemparamet_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
                this.LoadRrouteParameterData(this.dgv_listallcraftitemparamet["CraftInfoId", e.RowIndex].Value.ToString());
        }

        private void LoadRrouteParameterData(string CraftInfoId)
        {
            MyContextMenu mMycontextmenu =
                new MyContextMenu(this, this.lsCraftItemInfo[CraftInfoId].craftItemParametList);

            if (mMycontextmenu.ShowDialog() == DialogResult.OK)
            {
                this.lsCraftItemInfo[gStatus.dblStatusId.Split('_')[0]].craftItemParametList = this.gdatatable;
                for (int i = 0; i < this.lsCraftItemInfo[gStatus.dblStatusId.Split('_')[0]].craftItemParametList.Rows.Count; i++)
                {
                    if (this.lsCraftItemInfo[gStatus.dblStatusId.Split('_')[0]].craftItemParametList.Rows[i].RowState == DataRowState.Deleted)
                    {
                        this.lsCraftItemInfo[gStatus.dblStatusId.Split('_')[0]].craftItemParametList.Rows.RemoveAt(i);
                    }
                }
                this.UpdateCraftInfo();
                this.gdatatable = new DataTable();
            }
        }

        #region 流程图
        /// <summary>
        /// FlowStatus 的摘要说明。
        /// </summary>
        public class FlowStatus
        {
            /// <summary>
            /// 环节ID, STATUS_ID
            /// </summary>
            public string dblStatusId = string.Empty;
            //public Double dblStatusId = 0;
            /// <summary>
            /// 流程ID,FLOW_ID
            /// </summary>
            public string dblFlowId = string.Empty;
            // public Double dblFlowId;

            /// <summary>
            /// 环节类型ID,STATUS_TYPE_ID
            /// </summary>
            public enum eStatusTypeId
            {
                开始 = 0,
                结束 = 1,
                普通 = 2,
                子流 = 3,
                自动 = 4,
                分流 = 5,
                合流 = 6,
                转流程 = 7
            }
            /// <summary>
            /// 环节类型ID,STATUS_TYPE_ID:	 
            /// 0	开始;
            ///  1	结束;
            ///  2	普通;
            ///  3	子流;
            ///  4	自动;
            ///  5	分流;
            ///  6	合流;
            ///  7	转流程;
            /// </summary>
            public int intStatusTypeId;
            /// <summary>
            /// 矩形变量,用来判断某点是否在矩形内
            /// </summary>
            public System.Drawing.Rectangle objRect;
            /// <summary>
            /// 状态最后的位置X坐标
            /// </summary>
            public int last_x = -1;
            /// <summary>
            /// 状态最后位置的Y坐标
            /// </summary>
            public int last_y = -1;
            /// <summary>
            /// 判断是否可以移动,为零不能移动
            /// </summary>
            public int isMove = 0;
            /// <summary>
            /// 状态矩形左上角的X坐标
            /// </summary>
            public int _x = -1;
            /// <summary>
            /// 状态矩形左上角的Y坐标
            /// </summary>
            public int _y = -1;
            /// <summary>
            /// 状态矩形宽度
            /// </summary>
            public int _w = 0;
            /// <summary>
            /// 状态矩形高度
            /// </summary>
            public int _h = 0;
            /// <summary>
            /// 环节名称,STATUS_NAME
            /// </summary>
            public String strStatusName;
            /// <summary>
            /// 相关字段, INTERFIX_FIELD
            /// </summary>
            public String strInterfixField;
            /// <summary>
            ///  Com扩展, COM_EXTEND
            /// </summary>
            public String strComExtend;
            /// <summary>
            /// 子流:等待，不等待,CHILD_FLOW;当用0,1来表示时表示的是分流的单个和多个
            /// </summary>
            public String strChildFlow;
            /// <summary>
            /// 子流（分流）类型:同时、顺序,CHILD_FLOW_TYPE
            /// </summary>
            public String strChildFlowType;
            /// <summary>
            /// 子流提示, CHILD_FLOW_HINT
            /// </summary>
            public String strChildFlowHint;
            /// <summary>
            /// 合流自定义规则,INTERFLOW_UNITE_LIST
            /// </summary>
            public String strJoinRule2;
            /// <summary>
            /// 自动字段名称,AUTO_FIELD_NAME
            /// </summary>
            public String strAutoFieldName;
            /// <summary>
            /// 自动列表,AUTO_FIELD_LIST
            /// </summary>
            public String strAutoFieldList;
            /// <summary>
            /// 合流自定义规则,INTERFLOW_CUSTOM_RULE
            /// </summary>
            public String strJoinRule;
            /// <summary>
            /// 自动自定义规则, AUTO_CUSTOM_RULE 
            /// </summary>
            public String strAutoRule;
            /// <summary>
            /// 转流程流程名,CHANGE_FLOW_NAME
            /// </summary>
            public String strChangeFlowName;
            /// <summary>
            /// 转流程状态名称, CHANGE_STATUS_NAME
            /// </summary>
            public String strChangeStatusName;

            /// <summary>
            /// 上级节点集合
            /// </summary>
            public List<string> UpStatusId = new List<string>();
            /// <summary>
            /// 下级节点集合
            /// </summary>
            public List<string> NextStatusId = new List<string>();

            /// <summary>
            /// 是否显示该状态对象,删除时状态对象并不从ArrayList删除，只是不让其显示。
            ///当增加新状态对象时，如果arylist中有没有显示的状态对象，则取第一个来显示。这样可以保证序号的一致性
            ///即:当1，2，3，4加入画板后2删除后，新增的状态对象编号还是2，只是位置变换了而已。其他属性不变。
            ///状态对象的名称用其在ArrayList中的位置+1来确定
            /// </summary>
            public bool blDisplay = true;

            /// <summary>
            /// 通过状态的左上角x,y坐标和状态的宽度，高度构造一个状态对象
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <param name="w">宽度</param>
            /// <param name="h">高度</param>
            public FlowStatus(int x, int y, int w, int h)
            {
                _x = x;
                _y = y;
                _w = w;
                _h = h;

            }
            public FlowStatus()//从Xml生成空FlowStatus对象
            {

            }
            /// <summary>
            /// 判断一个点是否在状态对象内
            /// </summary>
            /// <param name="mousex"></param>
            /// <param name="mousey"></param>
            /// <returns>坐标点是否在状态对象内</returns>
            public bool contains(int mousex, int mousey)
            {
                objRect.X = this._x;
                objRect.Y = this._y;
                objRect.Width = this._w;
                objRect.Height = this._h;
                if (objRect.Contains(mousex, mousey)) return true;
                else return false;
            }

            /// <summary>
            /// 根据鼠标点更新状态对象坐标
            /// </summary>
            /// <param name="mousex"></param>
            /// <param name="mousey"></param>
            public void setLocation(int mousex, int mousey)
            {
                this._x = this.last_x + mousex;
                this._y = this.last_y + mousey;
            }

            /// <summary>
            /// 清除状态的属性
            /// 删除状态时，并不真正删除该对象，但是要清除其属性
            /// </summary>
            public void clearProperty()
            {
                blDisplay = false;
                last_x = -1;//状态最后的位置X坐标		
                last_y = -1;//状态最后位置的Y坐标
                isMove = 0;//判断是否可以移动,为零不能移动		
                _x = -1;//状态矩形左上角的X坐标		
                _y = -1;//状态矩形左上角的Y坐标
                this.dblStatusId = string.Empty;// -1;
                strStatusName = "";//环节名称,STATUS_NAME
                strInterfixField = "";// 相关字段, INTERFIX_FIELD
                strComExtend = "";// Com扩展, COM_EXTEND
                strChildFlow = "";//子流:等待，不等待,CHILD_FLOW;当用0,1来表示时表示的是分流的单个和多个
                strChildFlowType = "";//子流（分流）类型:同时、顺序,CHILD_FLOW_TYPE	
                strChildFlowHint = "";//子流提示, CHILD_FLOW_HINT
                strJoinRule2 = "";//合流自定义规则,INTERFLOW_UNITE_LIST	
                strAutoFieldName = "";//自动字段名称,AUTO_FIELD_NAME
                strAutoFieldList = "";//自动列表,AUTO_FIELD_LIST
                strJoinRule = ""; // 合流自定义规则,INTERFLOW_CUSTOM_RULE
                strAutoRule = "";//自动自定义规则, AUTO_CUSTOM_RULE 
                strChangeFlowName = "";//转流程流程名,CHANGE_FLOW_NAME
                strChangeStatusName = "";//转流程状态名称, CHANGE_STATUS_NAME	
            }
        }

        /// <summary>
        /// FlowDisposal 的摘要说明。
        /// </summary>
        public class FlowDisposal
        {
            /// <summary>
            /// 处理过程ID， DISPOSAL_ID
            /// </summary>
            public Double dblDisposalId = 0;
            /// <summary>
            /// 流程ID,FLOW_ID	
            /// </summary>

            public Double dblFlowId;
            /// <summary>
            /// 处理过程提示,DISPOSAL_HINT
            /// </summary>
            public String strDisposalHint;
            /// <summary>
            /// 处理过程名称,DISPOSAL_NAME
            /// </summary>
            public String strDisposalName;
            /// <summary>
            /// 经办状态,TRANSACT_STATUS_ID
            /// </summary>
            public Double dblTransactStatusId;
            /// <summary>
            /// 组限制,GROUP_LIMIT 
            /// </summary>
            public int intGroupLimit;
            //public Double dblCurStatusId;//当前环节ID,CUR_STATUS_ID
            //public Double dblPreStatusId;//上环节ID, PRE_STATUS_ID

            /// <summary>
            /// 当前环节ID,CUR_STATUS_ID
            /// </summary>
            public string dblCurStatusId = string.Empty;
            /// <summary>
            /// 上环节ID, PRE_STATUS_ID
            /// </summary>
            public string dblPreStatusId = string.Empty;

            /// <summary>
            /// X坐标,CUR_X 
            /// </summary>
            public int intCurX;
            /// <summary>
            /// Y坐标,CUR_Y
            /// </summary>
            public int intCurY;
            /// <summary>
            /// 画线的两个处理对象的坐标
            /// </summary>
            public int x1, y1, x2, y2;

            public int iWidth;
            public int iHeight;
            /// <summary>
            /// 代表一个途程对象的四个中点
            /// </summary>
            private class StatusPoint
            {
                /// <summary>
                /// 上中点
                /// </summary>
                public Point pUp = new Point();
                /// <summary>
                /// 下中点
                /// </summary>
                public Point pDown = new Point();
                /// <summary>
                /// 左中点
                /// </summary>
                public Point pLeft = new Point();
                /// <summary>
                /// 右中点
                /// </summary>
                public Point pRight = new Point();
            }

            /// <summary>
            /// 表示上一个途程的编号和其对应的四个中点
            /// </summary>
            private StatusPoint PreStatusPoints
            {
                get
                {
                    return new StatusPoint()
                    {
                        pUp = new Point(this.x1, this.y1 - (iHeight / 2)),
                        pDown = new Point(this.x1, this.y1 + (iHeight / 2)),
                        pLeft = new Point(this.x1 - (iWidth / 2), this.y1),
                        pRight = new Point(this.x1 + (iWidth / 2), this.y1)
                    };
                }
            }

            /// <summary>
            /// 表示下一个途程的编号和其对应的四个中点
            /// </summary>
            private StatusPoint CurStatusPoints
            {
                get
                {
                    return new StatusPoint()
                    {
                        pUp = new Point(this.x2, this.y2 - (iHeight / 2)),
                        pDown = new Point(this.x2, this.y2 + (iHeight / 2)),
                        pLeft = new Point(this.x2 - (iWidth / 2), this.y2),
                        pRight = new Point(this.x2 + (iWidth / 2), this.y2)
                    };
                }
            }

            /// <summary>
            /// 获取距离最近的点(上一个对象)
            /// </summary>
            public Point GetPreShortPoint(Point paim)
            {
                Point P = new Point();

                //先取X轴最近的点
                if (Math.Abs(paim.X - this.PreStatusPoints.pUp.X) < Math.Abs(paim.Y - this.PreStatusPoints.pLeft.Y))
                {
                    if (Math.Abs(paim.Y - this.PreStatusPoints.pUp.Y) < Math.Abs(paim.Y - this.PreStatusPoints.pDown.Y))
                        P = this.PreStatusPoints.pUp;
                    else
                        P = this.PreStatusPoints.pDown;
                }
                else
                {
                    if (Math.Abs(paim.X - this.PreStatusPoints.pLeft.X) < Math.Abs(paim.X - this.PreStatusPoints.pRight.X))
                        P = this.PreStatusPoints.pLeft;
                    else
                        P = this.PreStatusPoints.pRight;
                }
                return P;
            }

            /// <summary>
            /// 获取距离最近的点(下一个对象)
            /// </summary>
            public Point GetCurShortPoint(Point paim)
            {
                Point P = new Point();

                //先取X轴最近的点
                if (Math.Abs(paim.X - this.CurStatusPoints.pUp.X) < Math.Abs(paim.Y - this.CurStatusPoints.pLeft.Y))
                {
                    if (Math.Abs(paim.Y - this.CurStatusPoints.pUp.Y) < Math.Abs(paim.Y - this.CurStatusPoints.pDown.Y))
                        P = this.CurStatusPoints.pUp;
                    else
                        P = this.CurStatusPoints.pDown;
                }
                else
                {
                    if (Math.Abs(paim.X - this.CurStatusPoints.pLeft.X) < Math.Abs(paim.X - this.CurStatusPoints.pRight.X))
                        P = this.CurStatusPoints.pLeft;
                    else
                        P = this.CurStatusPoints.pRight;
                }
                return P;
            }

            /// <summary>
            /// 是否显示,为false不显示
            /// </summary>
            public bool blDisplay = true;

            /// <summary>
            /// lable标签,默认为为处理的名称
            /// </summary>
            public MyLable lbDispName;
            private int intMouseDownX;
            private int intMouseDownY;//鼠标在lable上按下的坐标
            /// <summary>
            /// lable的宽度
            /// </summary>
            public int intlblw = 40;
            /// <summary>
            /// lbale的高度
            /// </summary>
            public int intlblh = 20;
            private System.Windows.Forms.ContextMenu menuDisp;
            private System.Windows.Forms.MenuItem menuDel;
            private System.Windows.Forms.MenuItem menuSet;//lable标签对应的操作菜单	
            private System.Windows.Forms.MenuItem menuPassSet;
            private System.Windows.Forms.MenuItem menuFailSet;
            private System.Windows.Forms.MenuItem menuDoubleTest;

            public int intFormW;//画板的宽度，传递进来后防止lable出界
            public int intFormH;//画板的高度，传递进来后防止lable出界	
            public bool blCanMove = false;//lable是否可以移动

            public enum eRouteType
            {
                正常流程,
                维修流程,
                重测流程
            }
            public eRouteType gRoutetype;
            public List<FlowDisposal> aryDisp = new List<FlowDisposal>();
            public Dictionary<string, FlowStatus> aryStatusObjs = new Dictionary<string, FlowStatus>();
            public FlowStatus transactStatus;//经办状态对象;

            public FlowDisposal(FlowStatus objFrom, FlowStatus objTo)
            {
                int x1 = objFrom._x + Convert.ToInt32(objFrom._w / 2);
                int y1 = objFrom._y + Convert.ToInt32(objFrom._h / 2);
                int x2 = objTo._x + Convert.ToInt32(objTo._w / 2);
                int y2 = objTo._y + Convert.ToInt32(objTo._h / 2);
                this.x1 = x1;
                this.y1 = y1;
                this.x2 = x2;
                this.y2 = y2;
                int intCenterX = Convert.ToInt32((x1 + x2) / 2);
                int intCenterY = Convert.ToInt32((y1 + y2) / 2);
                this.intCurX = intCenterX;
                this.intCurY = intCenterY;
                this.dblPreStatusId = objFrom.dblStatusId;
                this.dblCurStatusId = objTo.dblStatusId;

                iWidth = objFrom._w;
                iHeight = objFrom._h;

                this.gRoutetype = eRouteType.正常流程;
                this.setLabelProp();
            }

            public FlowDisposal()//从xml生成空Disposal对象
            {

            }
            public void setLabelProp()//设置label的属性
            {
                this.lbDispName = new MyLable();
                this.lbDispName.Location =
                    new System.Drawing.Point(this.intCurX - Convert.ToInt32(this.intlblw / 2),
                        this.intCurY - Convert.ToInt32(this.intlblh / 2));
                this.lbDispName.Size = new System.Drawing.Size(this.intlblw, this.intlblh);
                this.lbDispName.Font = new System.Drawing.Font("宋体", 8.2F);

                this.lbDispName.Text = strDisposalName;
                if (this.gRoutetype == eRouteType.正常流程)
                    this.lbDispName.BackColor = Color.FromArgb(39, 50, 241);
                else if (this.gRoutetype == eRouteType.维修流程)
                    this.lbDispName.BackColor = Color.FromArgb(223, 86, 57);
                else
                    this.lbDispName.BackColor = Color.FromArgb(163, 73, 164);
                this.lbDispName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                this.lbDispName.MouseDown += new System.Windows.Forms.MouseEventHandler(lbl_MouseDown);
                this.lbDispName.MouseMove += new System.Windows.Forms.MouseEventHandler(lbl_MouseMove);
                this.lbDispName.MouseUp += new System.Windows.Forms.MouseEventHandler(lbl_MouseUp);
                //设置标签的右键菜单
                this.menuDisp = new System.Windows.Forms.ContextMenu();
                this.menuDel = new System.Windows.Forms.MenuItem();
                this.menuSet = new System.Windows.Forms.MenuItem();
                this.menuPassSet = new System.Windows.Forms.MenuItem();
                this.menuFailSet = new System.Windows.Forms.MenuItem();
                this.menuDoubleTest = new System.Windows.Forms.MenuItem();

                this.menuDel.Text = "删除";
                this.menuDel.Click += new System.EventHandler(this.menuDel_Click);
                this.menuSet.Text = "状态设置";

                this.menuPassSet.Text = "正常流程";
                this.menuPassSet.Click += new EventHandler(menuPassSet_Click);

                this.menuFailSet.Text = "维修流程";
                this.menuFailSet.Click += new EventHandler(menuFailSet_Click);

                this.menuDoubleTest.Text = "重测流程";
                this.menuDoubleTest.Click += new EventHandler(menuDoubleTest_Click);

                this.menuSet.MenuItems.AddRange(new MenuItem[] 
            { 
                this.menuPassSet, 
                this.menuFailSet,
                this.menuDoubleTest
            });


                this.menuDisp.MenuItems.Add(menuSet);
                this.menuDisp.MenuItems.Add(menuDel);
                this.lbDispName.ContextMenu = this.menuDisp;
            }

            public void menuDoubleTest_Click(object sender, EventArgs e)
            {
                if (this.gRoutetype != eRouteType.重测流程)
                {
                    foreach (FlowStatus fs in aryStatusObjs.Values)
                    {
                        if (fs.dblStatusId == this.dblPreStatusId)
                        {
                            if (fs.intStatusTypeId == 1)
                            {
                                MessageBoxEx.Show("结束节点不能有正常流出节点");
                                return;
                            }
                        }
                    }
                }
                this.gRoutetype = eRouteType.重测流程;
                this.lbDispName.BackColor = Color.FromArgb(163, 73, 164);
            }

            public void menuFailSet_Click(object sender, EventArgs e)
            {
                if (this.gRoutetype != eRouteType.维修流程)
                {
                    foreach (FlowDisposal item in aryDisp)
                    {
                        if (item.dblPreStatusId == this.dblCurStatusId &&
                            item.dblCurStatusId == this.dblPreStatusId)
                        {
                            MessageBoxEx.Show("该操作会导致死循环\n\n操作非法", "提示");
                            return;
                        }

                        if (this.aryStatusObjs[this.dblCurStatusId].intStatusTypeId == 1)
                        {
                            MessageBoxEx.Show("结束节点不能作为维修节点");
                            return;
                        }

                        if (this.aryStatusObjs[this.dblCurStatusId].intStatusTypeId == 0)
                        {
                            MessageBoxEx.Show("开始节点不能作为维修节点");
                            return;
                        }
                    }
                }
                this.gRoutetype = eRouteType.维修流程;
                this.lbDispName.BackColor = Color.FromArgb(223, 86, 57);
            }

            public void menuPassSet_Click(object sender, EventArgs e)
            {
                if (this.gRoutetype != eRouteType.正常流程 && this.gRoutetype != eRouteType.重测流程)
                {
                    foreach (FlowDisposal item in aryDisp)
                    {
                        if (item.dblPreStatusId == this.dblCurStatusId &&
                            item.dblCurStatusId == this.dblPreStatusId)
                        {
                            MessageBoxEx.Show("该操作会导致死循环\n\n操作非法", "提示");
                            return;
                        }
                    }

                    foreach (FlowStatus fs in aryStatusObjs.Values)
                    {
                        if (fs.dblStatusId == this.dblPreStatusId)
                        {
                            if (fs.intStatusTypeId == 1)
                            {
                                MessageBoxEx.Show("结束节点不能有正常流出节点");
                                return;
                            }
                        }
                    }
                }
                this.gRoutetype = eRouteType.正常流程;
                this.lbDispName.BackColor = Color.FromArgb(39, 50, 241);
            }

            public void getFormwh(int intW, int intH)//得到FlowMap Form的高度和宽度
            {
                this.intFormW = intW;
                this.intFormH = intH;
            }

            private void lbl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
            {
                this.intMouseDownX = this.intCurX - e.X;
                this.intMouseDownY = this.intCurY - e.Y;
                this.blCanMove = true;

            }

            private void lbl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
            {
                if (this.blCanMove)
                {
                    this.intCurX = this.intMouseDownX + e.X;
                    this.intCurY = this.intMouseDownY + e.Y;
                    //this.lbDispName.Location=new Point(this.intCurX-Convert.ToInt32(this.intlblw/2) , this.intCurY-Convert.ToInt32(this.intlblh/2) );	
                    // this.lbDispName.Refresh();
                }

            }

            private void lbl_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
            {
                this.intCurX = this.intMouseDownX + e.X;
                this.intCurY = this.intMouseDownY + e.Y;
                this.lbDispName.Location = new Point(this.intCurX - Convert.ToInt32(this.intlblw / 2), this.intCurY - Convert.ToInt32(this.intlblh / 2));
                checkPosition(lbDispName, this.intFormW, this.intFormH);
                this.intCurX = lbDispName.Left + Convert.ToInt32(this.intlblw / 2);
                this.intCurY = lbDispName.Top + Convert.ToInt32(this.intlblh / 2);
                this.blCanMove = false;
            }

            //让lable始终在画板内,intFormW为画板的宽度,intFormH为画板的高度
            public void checkPosition(System.Windows.Forms.Label objLable, int intFormW, int intFormH)
            {
                int w = intFormW;
                int h = intFormH;
                int new_x = objLable.Left;
                int new_y = objLable.Top;
                int lblW = objLable.Width; ;
                int lblH = objLable.Height;


                if ((objLable.Left + lblW) > w)
                {
                    new_x = (int)w - lblW;
                }
                if (objLable.Left < 0)
                {
                    new_x = 0;
                }
                if ((objLable.Top + lblH) > h)
                {
                    new_y = (int)h - lblH;
                }
                if (objLable.Top < 0)
                {
                    new_y = 0;
                }
                objLable.Left = new_x;
                objLable.Top = new_y;
            }
            private void menuDel_Click(object sender, System.EventArgs e)//删除一个处理对象
            {
                if (MessageBoxEx.Show(string.Format("是否确定删除:[{0}]节点?\n\n确认[Yes] 取消[No]", this.lbDispName.Text),
                    "提示", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Asterisk) == DialogResult.No)
                    return;
                this.clearProperty();
            }

            private void menuSet_Click(object sender, System.EventArgs e)//设置处理对象的属性
            {
                //frmDispSet frmSet = new frmDispSet();
                //frmSet.setThisDisposal(this);
                //frmSet.setAryStatus(this.aryStatusObjs);
                //frmSet.setAryDisposal(this.aryDisp)
                //frmSet.MaximizeBox = false;
                //frmSet.MinimizeBox = false;
                //frmSet.ShowDialog();
            }

            /**
             * 清除处理对象的所有属性
             * 删除处理对象时，并不真正删除该对象，但是要清除其属性
             * 	
             * */
            public void clearProperty()
            {
                this.blDisplay = false;//是否显示,为false不显示
                this.lbDispName.Visible = false;
                strDisposalHint = "";//处理过程提示,DISPOSAL_HINT
                strDisposalName = "";//处理过程名称,DISPOSAL_NAME  
                dblTransactStatusId = 0;//经办状态,TRANSACT_STATUS_ID   
                intGroupLimit = 0;//组限制,GROUP_LIMIT  
                dblCurStatusId = string.Empty;// -1;//当前环节ID,CUR_STATUS_ID
                dblPreStatusId = string.Empty;// -1;//上环节ID, PRE_STATUS_ID		
                intCurX = -1;//X坐标,CUR_X 
                intCurY = -1;//Y坐标,CUR_Y	
                blCanMove = false;//lable是否可以移动
                //aryDisp=null;
            }
        }
        #endregion

        private void dgv_showwoinfo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != -1 && e.RowIndex != -1)
                {
                    if (this.dgv_showwoinfo["routgroupId", e.RowIndex].Value != null &&
                        !string.IsNullOrEmpty(this.dgv_showwoinfo["routgroupId", e.RowIndex].Value.ToString()))
                    {
                        if (!string.IsNullOrEmpty(this.gRoutegroupId))
                        {
                            this.SaveRouteBitmap();
                            DialogResult result = MessageBoxEx.Show(
                          string.Format("是否确认保存流程:\n\n流程编号[{0}]\n流程名称:[{1}]\n\n保存[Yes] 不保存[No] 取消[Cancel]",
                          this.gRoutegroupId, this.gRoutegroupname),
                          "提示",
                          MessageBoxButtons.YesNoCancel,
                          MessageBoxIcon.Asterisk);
                            if (result == DialogResult.Cancel)
                                return;
                            if (result == DialogResult.Yes)
                            {
                                Thread thread = new Thread(new ThreadStart(this.SaveRouteToDb));
                                thread.Start();
                                //this.SaveRouteToDb();
                            }
                        }
                        if (string.IsNullOrEmpty(this.dgv_showwoinfo["routgroupId", e.RowIndex].Value.ToString()))
                            return;

                        string err = FrmBLL.publicfuntion.ChktEditing(this.dgv_showwoinfo["routgroupId", e.RowIndex].Value.ToString(), this.strFnumName, this.mFrm.gUserInfo.userId, this.mFrm.gUserInfo.username);


                        if (err != "OK")
                        {
                            if (err.IndexOf("ERROR") != -1)
                            {
                                this.mFrm.ShowPrgMsg(err, MainParent.MsgType.Error);
                                return;
                            }
                            else
                            {
                                MessageBoxEx.Show(string.Format("用户:[{0}]/[{1}]--{2}正在编辑中,您现在不能进行修改!!",
     err.Split(':')[1].Split(',')[0], err.Split(':')[1].Split(',')[1], err.Split(':')[1].Split(',')[2]), "提示!!");
                                return;
                            }
                        }
                        this.LoadXmlRoute(this.dgv_showwoinfo["routgroupId", e.RowIndex].Value.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void bt_selectwo_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.tb_woname.Text))
                    FillDataGridView(FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWoInfo.Instance.GetWoInfo(this.tb_woname.Text.Trim(),null,null)));
                else
                    FillDataGridView(FrmBLL.ReleaseData.arrByteToDataTable(RefWebService_BLL.refWebtWoInfo.Instance.GetWoInfo(null,null,null)));
            }
            catch (Exception ex)
            {
                mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void FillDataGridView(DataTable dt)
        {
            this.dgv_showwoinfo.Invoke(new EventHandler(delegate
            {
                this.dgv_showwoinfo.DataSource = dt;
            }));
        }

        private void btLsave_Click(object sender, EventArgs e)
        {
            RoutegroupAtt ra = new RoutegroupAtt(this);
            if (ra.ShowDialog() == DialogResult.OK)
            {
                this.bt_saveroute_Click(sender, e);
            }
        }

        private void bt_manageroute_Click(object sender, EventArgs e)
        {
            ManageRoute mr = new ManageRoute(mFrm);
            mr.ShowDialog();
        }

        private void CreateRoute_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                refwebtEditing.Instance.DeletetEditingByUserIdAndPrj(this.mFrm.gUserInfo.userId, this.strFnumName);
            }
            catch
            {
            }
        }

        private void tbSelectCraftId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                buttonX1_Click(null, null);
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.iIsCraftOrRoute == 0)
                    ShowCraftList(FrmBLL.publicfuntion.getNewTable(this.mCraftIdTable, string.Format("craftname like '{0}%'", this.tbSelectCraftId.Text.Trim())));
                else
                    ShowRouteList(FrmBLL.publicfuntion.getNewTable(this.mRouteIdTable, string.Format("routgroupdesc like '{0}%'", this.tbSelectCraftId.Text.Trim())));
            }
            catch (Exception ex)
            {
                this.mFrm.ShowPrgMsg(ex.Message, MainParent.MsgType.Error);
            }
        }

        private void tb_woname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (string.IsNullOrEmpty(this.tb_woname.Text))
                    return;
                bt_selectwo_Click(null, null);
            }
        }
    }

    /// <summary>
    /// 画矩形类(可以圆角)
    /// </summary>
    public class MyDrawRectangle
    {
        /// <summary>
        /// 画矩形
        /// </summary>
        /// <param name="g">画板</param>
        /// <param name="pen">画笔</param>
        /// <param name="rect">矩形</param>
        /// <param name="cornerRadius">圆角值</param>
        public static void DrawRoundRectangle(Graphics g, Pen pen, Rectangle rect, int cornerRadius)
        {
            using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius))
            {
                g.DrawPath(pen, path);
            }
        }
        /// <summary>
        /// 填充矩形
        /// </summary>
        /// <param name="g">画板</param>
        /// <param name="brush">画刷</param>
        /// <param name="rect">矩形</param>
        /// <param name="cornerRadius">圆角值</param>
        public static void FillRoundRectangle(Graphics g, Brush brush, Rectangle rect, int cornerRadius)
        {
            using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius))
            {
                g.FillPath(brush, path);
            }
        }
        /// <summary>
        /// 创建一个矩形对象
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="cornerRadius"></param>
        /// <returns></returns>
        internal static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            roundedRect.CloseFigure();
            return roundedRect;
        }

    }

    public class MyLable : Label
    {
        public MyLable()
        {
            //this.BackColor = Color.White;
            //Graphics e = this.CreateGraphics();
            ////e.DrawRectangle(new Pen(Color.Red), 0, 0, this.Width, this.Height);
            //MyDrawRectangle.DrawRoundRectangle(e, new Pen(Color.Blue), new Rectangle { X = 0, Y = 0, Width = this.Width, Height = this.Height }, 10);
            //MyDrawRectangle.FillRoundRectangle(e, new SolidBrush(Color.Blue), new Rectangle { X = 0, Y = 0, Width = this.Width, Height = this.Height }, 10);
            //e.DrawString(this.Text, new Font("宋体", 9), new SolidBrush(Color.White), 0, 0);
            //this.BackColor = Color.Transparent;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            //e.Graphics.DrawRectangle(new Pen(Color.Red), 0, 0, this.Width, this.Height);
            //MyDrawRectangle.DrawRoundRectangle(e.Graphics, new Pen(Color.Blue), new Rectangle { X = 0, Y = 0, Width = this.Width, Height = this.Height }, 10);
            //MyDrawRectangle.FillRoundRectangle(e.Graphics, new SolidBrush(Color.Blue), new Rectangle { X = 0, Y = 0, Width = this.Width, Height = this.Height }, 10);
            //e.Graphics.DrawString("", new Font("宋体", 9), new SolidBrush(Color.White), 2, 2);
            //this.BackColor = Color.Transparent;
            base.OnPaint(e);
        }
    }
}
