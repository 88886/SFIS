/*author:   l_dragon     email:fly_dragon2000:hotmail.com 
*/ 
using   System; 
using   System.Drawing; 
using   System.Collections; 
using   System.ComponentModel; 
using   System.Windows.Forms; 
using   System.Data;

namespace moverect
{
    ///   <summary> 
    ///   Form1   的摘要说明。 
    ///   </summary> 
    public partial class Form2 : System.Windows.Forms.Form
    {
        ///   <summary> 
        ///   必需的设计器变量。 
        ///   </summary> 
        public FlowStatus gStatus;//在Form中被选中的唯一节点，此处没有多个节点。如果有多个节点用arrayList保存节点列表。 


        public Form2()
        {
            InitializeComponent();
        }


        /** 
         在Form载入时建立一个节点对象，画出该节点 
        *     :param   无                 *     
        * <br> 处理步骤如下: 
        * <br> 在坐标100,100处，建立一个宽100，高50的节点对象。 
        * <br> 调用画图方法，画出该节点 
        **/
        private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            FlowStatus objStatus = new FlowStatus(100, 100, 100, 50);
            objStatus.intStatusTypeId = 0;
            objStatus.dblStatusId = 1;
            objStatus.strStatusName = "状态1 ";
            this.gStatus = objStatus;
            repaint();
        }


        public void myMouseDown(object sender, MouseEventArgs ex)
        {
            int intClickCount = ex.Clicks;//鼠标的点击次数 

            if (ex.Button == MouseButtons.Left)//左键，有对右键和双击的处理，此处省略 
            {
                if (intClickCount == 1)
                {
                    this.drawMouseObjs(ex, 1);// 
                }
                #region xxx
                /* 
                if(intClickCount==2   )//鼠标左键双击 
                { 
                        this.gStatus=this.SelectStatus(ex.X,ex.Y);//选中了某状态对象  
                        if(gStatus!=null) 
                        { 
                                                
                                frmStatusSet   frmSet=new   frmStatusSet(); 
                                frmSet.setThisStatus(this.gStatus); 
                                frmSet.setAryStatus(this.aryStatus);         
                                frmSet.MaximizeBox=false; 
                                frmSet.MinimizeBox=false;                                         
                                frmSet.ShowDialog();   
                                repaint(); 
                                                
                                                
                        } 
                } 
                */
                #endregion
            }
        }
        public void myMouseMove(object sender, MouseEventArgs ex)
        {
            if (ex.Button == MouseButtons.Left)
            {
                this.drawMouseObjs(ex, 2);

            }


        }
        public void myMouseUp(object sender, MouseEventArgs ex)
        {
            if (ex.Button == MouseButtons.Left)
            {
                this.drawMouseObjs(ex, 3);
            }
        }


        /// <summary>
        /// 鼠标事件触发时画图的处理,
        /// intEvent等于1.代表鼠标按下.intEvent等于2.代表鼠标移动.intEvent等于3.代表鼠标松开;
        /// 处理步骤如下: 
        ///*<br> intEvent等于1，鼠标按下时如果鼠标的坐标在某状态内，更新状态的last_x,last_y坐标(用来在以移动后更新状态坐标位置) 
        ///* 该状态可以移动 
        ///*<br> intEvent等于2，如果状态可以移动，更新状态位置 
        ///*<br> intEvent等于3，如果状态可以移动，更新状态位置，设置该状态为不能移动。
        /// </summary>
        /// <param name="ex">鼠标事件</param>
        /// <param name="intEvent">鼠标事件类型</param>
        public void drawMouseObjs(MouseEventArgs ex, int intEvent)
        {
            FlowStatus objStatus = this.gStatus;
            switch (intEvent)
            {
                case 1://mouseDown() 
                    if (objStatus.contains(ex.X, ex.Y))//按下的鼠标坐标在节点的范围内。 
                    {
                        Graphics g = this.CreateGraphics();
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
                        g.FillRectangle(new SolidBrush(Color.White), x1 + 6, y1 + 6, w1, h1);   //用白色擦掉画好的节点         
                        g.DrawRectangle(new Pen(Color.Black), x1, y1, w1, h1);//给节点加个黑边框。代表选中了该节点，可以拖动了。         
                        g.Dispose();
                        return;
                    }
                    break;

                case 2://mouseMove() 
                    if (objStatus.isMove == 1)
                    {
                        Graphics g = this.CreateGraphics();
                        int x1 = objStatus._x;
                        int y1 = objStatus._y;
                        int w1 = objStatus._w;
                        int h1 = objStatus._h;
                        g.DrawRectangle(new Pen(Color.White), x1, y1, w1, h1);           //用白色擦掉拖动时上一位置的节点                 
                        objStatus.setLocation(ex.X, ex.Y);
                        //         this.checkStatus(objStatus);//防止节点出画板外，省略。 
                        int x2 = objStatus._x;
                        int y2 = objStatus._y;
                        Pen penBlack = new Pen(Color.Black);
                        g.DrawRectangle(penBlack, x2, y2, w1, h1);   //在新位置画新的带黑边框的节点                                                         
                        g.Dispose();
                        return;

                    }
                    break;
                case 3://mouseUp() 
                    if (objStatus.isMove == 1)
                    {
                        updateLocation(ex, objStatus);//更新节点位置 
                        objStatus.isMove = 0;
                        repaint();//在新位置处，重新画一个带颜色和阴影及节点说明的矩形框。 
                        return;
                    }
                    break;
            }//end   switch 

        }


        /** 
            *     拖拽后更新状态和处理的位置。 
        *     :param   MouseEventArgs   e   鼠标事件 
        *     :param   FlowStatus   objStatus   状态对象 
        * <br> 处理步骤如下: 
        * <br> 确定托拽后的新坐标 
        * <br> 判定新状态是否在面板坐标内，如果不在，将其改变在面板内。 
        * <br> 更新状态的新位置 
        * <br> 更新与状态关联的处理的新位置。 
        **/
        public void updateLocation(MouseEventArgs e, FlowStatus objStatus)
        {

            objStatus.setLocation(e.X, e.Y);
            //this.checkStatus(objStatus);//节点是否在画板内，省略 
            int PosX = objStatus._x + Convert.ToInt32(objStatus._w / 2);
            int PosY = objStatus._y + Convert.ToInt32(objStatus._h / 2);
            double dblStatusId = objStatus.dblStatusId;
        }


        public void repaint()//画图 
        {
            Graphics g = this.CreateGraphics();
            //先将图形所有区域用白色填充                                 
            Pen WhitePen = new Pen(Color.White);
            g.DrawRectangle(WhitePen, 0, 0, this.Width, this.Height);
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, this.Width, this.Height);
            //填充完毕 

            int intType = this.gStatus.intStatusTypeId;
            string strType = " ";
            switch (intType)
            {
                case 0:
                    strType = "开始 ";
                    break;
                case 1:
                    strType = "结束 ";
                    break;
                case 2:
                    strType = "普通 ";
                    break;
                case 3:
                    strType = "子流 ";
                    break;
                case 4:
                    strType = "自动 ";
                    break;
                case 5:
                    strType = "分流 ";
                    break;
                case 6:
                    strType = "合流 ";
                    break;
                case 7:
                    strType = "转流程 ";
                    break;
            }//节点类型处理 

            int x1 = gStatus._x;
            int y1 = gStatus._y;
            int w1 = gStatus._w;
            int h1 = gStatus._h;
            string strName = gStatus.strStatusName;//节点名称 

            Pen penBlack = new Pen(Color.Black, 2);
            g.DrawRectangle(penBlack, x1, y1, w1, h1);
            //画节点的阴影。
            g.FillRectangle(new SolidBrush(Color.FromArgb(30, 30, 30)), x1 + 6, y1 + 6, w1, h1);
            //用颜色填充上半部分，用来说明节点的类型
            g.FillRectangle(new SolidBrush(Color.Orange), x1, y1, w1, Convert.ToInt32(h1 * 0.6));
            //用颜色画下半部分，用来说明是第几个节点。 
            g.FillRectangle(new SolidBrush(Color.Blue), x1, y1 + Convert.ToInt32((h1 * 0.6)),
                w1, Convert.ToInt32(h1 * 0.4));


            Font drawFont = new Font("宋体 ", 9);
            StringFormat drawFormat = new StringFormat();

            drawFormat.Alignment = StringAlignment.Center;
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            RectangleF drawRect = new RectangleF(x1, y1, w1, Convert.ToInt32(h1 * 0.6));
            RectangleF drawRect2 = new RectangleF(x1, y1 + Convert.ToInt32(h1 * 0.6), w1, Convert.ToInt32(h1 * 0.4));
            g.DrawString(strType, drawFont, drawBrush, drawRect, drawFormat);//在上半部分添加节点类型说明文字                         
            g.DrawString(strName, drawFont, new SolidBrush(Color.White), drawRect2, drawFormat);//在下半部分添加节点编号。 
            g.Dispose();//释放graphics对象。 
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

    }
}


namespace moverect
{
    ///   <summary> 
    ///   FlowStatus   的摘要说明。 
    ///   </summary> 
    public class FlowStatus
    {
        /// <summary>
        /// 环节ID,   STATUS_ID
        /// </summary>
        public Double dblStatusId = 0;// 
        /// <summary>
        /// 流程ID,FLOW_ID
        /// </summary>
        public Double dblFlowId;// 

        /// <summary>
        ///  环节类型ID,STATUS_TYPE_ID           
        ///       0         开始 
        ///        1         结束 
        ///        2         普通 
        ///        3         子流 
        ///        4         自动 
        ///        5         分流 
        ///        6         合流 
        ///        7         转流程 
        /// </summary>
        public int intStatusTypeId;
        /// <summary>
        /// 矩形变量,用来判断某点是否在矩形内
        /// </summary>
        public System.Drawing.Rectangle objRect;
        /// <summary>
        /// 状态最后的位置X坐标
        /// </summary>
        public int last_x = -1;// 
        /// <summary>
        /// 状态最后位置的Y坐标
        /// </summary>
        public int last_y = -1;// 

        /// <summary>
        /// 判断是否可以移动,为零不能移动
        /// </summary>
        public int isMove = 0;//   

        /// <summary>
        /// 状态矩形左上角的X坐标
        /// </summary>
        public int _x = -1;//  

        /// <summary>
        /// 状态矩形左上角的Y坐标
        /// </summary>
        public int _y = -1;// 

        /// <summary>
        /// 状态矩形宽度
        /// </summary>
        public int _w = 0;// 

        /// <summary>
        /// 状态矩形高度
        /// </summary>
        public int _h = 0;//    

        /// <summary>
        /// 环节名称,STATUS_NAME
        /// </summary>
        public String strStatusName;// 

        /// <summary>
        /// 是否显示该状态对象,删除时状态对象并不从ArrayList删除，只是不让其显示。 
        /// 当增加新状态对象时，如果arylist中有没有显示的状态对象，则取第一个来显示。这样可以保证序号的一致性 
        /// 即:当1，2，3，4加入画板后2删除后，新增的状态对象编号还是2，只是位置变换了而已。其他属性不变。 
        /// 状态对象的名称用其在ArrayList中的位置+1来确定 
        /// </summary>
        public bool blDisplay = true;

        /// <summary>
        ///通过状态的左上角x,y坐标和状态的宽度，高度构造一个状态对象 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w">宽度</param>
        /// <param name="h">高度,构造成功后dblMaxId自动加1。</param>
        public FlowStatus(int x, int y, int w, int h)
        {//普通状态 
            _x = x;
            _y = y;
            _w = w;
            _h = h;

        }
        /// <summary>
        /// 重载构造方法，从xml构造
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w">宽度</param>
        /// <param name="h">高度</param>
        /// <param name="fromXml"></param>
        public FlowStatus(int x, int y, int w, int h, int fromXml)//重载构造方法，从xml构造 
        {//普通状态 
            _x = x;
            _y = y;
            _w = w;
            _h = h;
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
        ///清除状态的属性   
        ///删除状态时，并不真正删除该对象，但是要清除其属性 
        /// </summary>
        public void clearProperty()
        {
            blDisplay = false;
            last_x = -1;//状态最后的位置X坐标                 
            last_y = -1;//状态最后位置的Y坐标 
            isMove = 0;//判断是否可以移动,为零不能移动                 
            _x = -1;//状态矩形左上角的X坐标                 
            _y = -1;//状态矩形左上角的Y坐标 
            this.dblStatusId = -1;
            strStatusName = " ";//环节名称,STATUS_NAME 
        }
    }
}

