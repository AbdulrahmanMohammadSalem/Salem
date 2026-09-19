using Salem.Controls.Properties;
using Salem.Drawing;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace Salem.Controls.Renderers {
    public class SalToolStripRenderer : ToolStripProfessionalRenderer {
        //----    Fields:
        private int _itemImageMargin = 0;

        //----    Properties:
        public Color TopLevelItemBackColor { get; set; } = SystemColors.Control;

        public Color ItemMouseOverBackColor { get; set; } = Color.FromArgb(179, 215, 243);

        public Color ItemMouseDownBackColor { get; set; } = Color.FromArgb(158, 204, 240);

        public Color ItemBorderColor { get; set; } = Color.FromArgb(0, 120, 215);

        public Color ItemForeColor { get; set; } = SystemColors.WindowText;

        public Color ItemMouseOverForeColor { get; set; } = SystemColors.WindowText;

        public Color DroppedDownSurfaceBackColor { get; set; } = SystemColors.Window;

        public Color DroppedDownSurfaceBorderColor { get; set; } = Color.FromArgb(128, 128, 128);

        public Color ImageMarginBackColor { get; set; } = Color.FromArgb(246, 246, 246);

        public int ItemBorderSize { get; set; } = 1;

        public ArrowStyles DropDownArrowStyle { get; set; } = ArrowStyles.FilledTriangle;

        public Color DropDownArrowColor { get; set; } = SystemColors.WindowText;

        public Color MouseOverDropDownArrowColor { get; set; } = SystemColors.WindowText;

        public float DropDownArrowSize { get; set; } = 5F;

        public Image ItemCheckImage { get; set; } = Resources.Item_Check;

        public Color ToolStripBorderColor { get; set; } = Color.FromArgb(150, 150, 150);

        public Color SeparatorForeColor { get; set; } = SystemColors.ControlDark;

        public BorderSides ToolStripBorderSides { get; set; } = BorderSides.None;

        public bool UseCustomItemCheckImage { get; set; } = false;

        public bool UseFullWidthSeparator { get; set; } = false;

        public bool IgnoreItemStyleProperties { get; set; } = false;

        //----    Method Overrides:
        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e) {
            var _btn = e.Item as ToolStripButton;
            var _rect = new Rectangle(0, 0, _btn.Bounds.Width - 1, _btn.Bounds.Height - 1);

            if (_btn.Pressed) {
                DrawingHelpers.FillRect(e.Graphics, DrawingHelpers.TranslateOrigin(_btn.Bounds), ItemMouseDownBackColor);
                DrawingHelpers.DrawSimpleBorder(e.Graphics, _rect, ItemBorderColor, ItemBorderSize);
            } else if (_btn.Selected) {
                DrawingHelpers.FillRect(e.Graphics, DrawingHelpers.TranslateOrigin(_btn.Bounds), ItemMouseOverBackColor);
                DrawingHelpers.DrawSimpleBorder(e.Graphics, _rect, ItemBorderColor, ItemBorderSize);
            }
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e) {
            var _item = e.Item as ToolStripMenuItem;
            bool _topLevelItem = _item.OwnerItem == null && !(e.ToolStrip is ContextMenuStrip);

            if (_item.DropDown.Visible && _topLevelItem) {
                using (var _brush = new SolidBrush(DroppedDownSurfaceBackColor))
                    e.Graphics.FillRectangle(_brush, DrawingHelpers.TranslateOrigin(_item.Bounds));

                using (var _pen = new Pen(DroppedDownSurfaceBorderColor)) {
                    if (e.ToolStrip.PointToClient(_item.DropDown.Bounds.Location).X == _item.Bounds.X) {
                        if (_item.DropDownDirection == ToolStripDropDownDirection.AboveLeft || _item.DropDownDirection == ToolStripDropDownDirection.AboveRight) {
                            e.Graphics.DrawLines(_pen, new Point[] {
                                new Point(0, 0),
                                new Point(0, _item.Height - 1),
                                new Point(_item.Width - 1, _item.Height - 1),
                                new Point(_item.Width - 1, 0)
                            });
                        } else {
                            e.Graphics.DrawLines(_pen, new Point[] {
                                new Point(0, _item.Height - 1),
                                new Point(0, 0),
                                new Point(_item.Width - 1, 0),
                                new Point(_item.Width - 1, _item.Height - 1)
                            });
                        }
                    } else
                        DrawingHelpers.DrawSimpleBorder(e.Graphics, new Rectangle(0, 0, _item.Width - 1, _item.Height - 1), DroppedDownSurfaceBorderColor, ItemBorderSize);
                }
            } else {
                if (_item.Selected) {
                    DrawingHelpers.FillRect(e.Graphics, DrawingHelpers.TranslateOrigin(_item.Bounds), ItemMouseOverBackColor);
                    DrawingHelpers.DrawSimpleBorder(e.Graphics, new Rectangle(0, 0, _item.Bounds.Width - 1, _item.Bounds.Height - 1), ItemBorderColor, ItemBorderSize);
                } else {
                    if (_topLevelItem)
                        DrawingHelpers.FillRect(e.Graphics, DrawingHelpers.TranslateOrigin(_item.Bounds), IgnoreItemStyleProperties ? TopLevelItemBackColor : _item.BackColor);
                    else {
                        Color _backColor = IgnoreItemStyleProperties ? DroppedDownSurfaceBackColor : _item.BackColor;

                        if (e.Item.RightToLeft == RightToLeft.Yes)
                            DrawingHelpers.FillRect(e.Graphics, new Rectangle(0, 0, _item.Width - _itemImageMargin, _item.Height), _backColor);
                        else
                            DrawingHelpers.FillRect(e.Graphics, new Rectangle(_itemImageMargin - 1, 0, _item.Width - _itemImageMargin, _item.Height), _backColor);
                    }
                }
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e) {
            e.TextColor = e.Item.Selected ? ItemMouseOverForeColor : (IgnoreItemStyleProperties ? ItemForeColor : e.Item.ForeColor);
            base.OnRenderItemText(e);
        }

        protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e) {
            var _btn = e.Item as ToolStripDropDownButton;
            var _rect = new Rectangle(0, 0, _btn.Bounds.Width - 1, _btn.Bounds.Height - 1);

            if (_btn.Pressed) {
                DrawingHelpers.FillRect(e.Graphics, _rect, ItemMouseDownBackColor);
                DrawingHelpers.DrawSimpleBorder(e.Graphics, _rect, ItemBorderColor, ItemBorderSize);
            } else if (_btn.Selected) {
                DrawingHelpers.FillRect(e.Graphics, _rect, ItemMouseOverBackColor);
                DrawingHelpers.DrawSimpleBorder(e.Graphics, _rect, ItemBorderColor, ItemBorderSize);
            }
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e) {
            if (e.Item.Enabled)
                e.ArrowColor = e.Item.Selected ? MouseOverDropDownArrowColor : DropDownArrowColor;

            e.ArrowRectangle = new Rectangle(e.ArrowRectangle.X, e.ArrowRectangle.Y + 1, e.ArrowRectangle.Width, e.ArrowRectangle.Height);

            using (var _brush = new SolidBrush(e.ArrowColor))
            using (var _font = new Font("Segoe MDL2 Assets", DropDownArrowSize))
            using (var _stringFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                e.Graphics.DrawString(DrawingHelpers.GetArrowChar(DrawingHelpers.MapToBasicDirections(e.Direction), DropDownArrowStyle).ToString(), _font, _brush, e.ArrowRectangle, _stringFormat);
        }

        protected override void OnRenderItemBackground(ToolStripItemRenderEventArgs e) {
            var _item = e.Item as ToolStripMenuItem;
            var _rect = new Rectangle(0, 0, _item.Bounds.Width - 1, _item.Bounds.Height - 1);

            if (_item.Pressed) {
                DrawingHelpers.FillRect(e.Graphics, DrawingHelpers.TranslateOrigin(_item.Bounds), ItemMouseDownBackColor);
                DrawingHelpers.DrawSimpleBorder(e.Graphics, _rect, ItemBorderColor, ItemBorderSize);
            } else if (_item.Selected) {
                DrawingHelpers.FillRect(e.Graphics, DrawingHelpers.TranslateOrigin(_item.Bounds), ItemMouseOverBackColor);
                DrawingHelpers.DrawSimpleBorder(e.Graphics, _rect, ItemBorderColor, ItemBorderSize);
            }
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e) {
            Color _backColor;

            if (e.Item.OwnerItem == null && !(e.ToolStrip is ContextMenuStrip))
                _backColor = IgnoreItemStyleProperties ? TopLevelItemBackColor : e.Item.BackColor;
            else
                _backColor = IgnoreItemStyleProperties ? DroppedDownSurfaceBackColor : e.Item.BackColor;

            using (var _brush = new SolidBrush(_backColor)) {
                if (e.ToolStrip.RightToLeft == RightToLeft.Yes)
                    e.Graphics.FillRectangle(_brush, new Rectangle(0, 0, e.Item.Width - _itemImageMargin + 1, e.Item.Height));
                else
                    e.Graphics.FillRectangle(_brush, new Rectangle(_itemImageMargin - 3, 0, e.Item.Width - _itemImageMargin + 3, e.Item.Height));
            }

            using (Pen _pen = new Pen(IgnoreItemStyleProperties ? SeparatorForeColor : e.Item.ForeColor)) {
                if (e.Vertical) {
                    int _halfWidth = e.Item.Width / 2;
                    e.Graphics.DrawLine(_pen, _halfWidth, e.Item.ContentRectangle.Top, _halfWidth, e.Item.ContentRectangle.Bottom);
                } else {
                    int _halfHeight = e.Item.Height / 2;

                    if (UseFullWidthSeparator)
                        e.Graphics.DrawLine(_pen, 0, _halfHeight, e.ToolStrip.Width, _halfHeight);
                    else {
                        int _left = e.ToolStrip.Padding.Left;
                        int _right = e.ToolStrip.Width - e.ToolStrip.Padding.Right;

                        e.Graphics.DrawLine(_pen, _left, _halfHeight, _right, _halfHeight);
                    }
                }
            }
        }

        protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e) {
            var _btn = e.Item as ToolStripSplitButton;
            bool _alreadyPainted = false;
            var _rect = new Rectangle(0, 0, _btn.Bounds.Width - 1, _btn.Bounds.Height - 1);

            using (var _pen = new Pen(ItemBorderColor)) {
                if (_btn.ButtonPressed) {
                    DrawingHelpers.FillRect(e.Graphics, _btn.ButtonBounds, ItemMouseDownBackColor);
                    DrawingHelpers.FillRect(e.Graphics, _btn.DropDownButtonBounds, ItemMouseOverBackColor);
                    DrawingHelpers.DrawSimpleBorder(e.Graphics, _rect, ItemBorderColor, ItemBorderSize);

                    DrawingHelpers.FillRect(e.Graphics, _btn.SplitterBounds, ItemBorderColor);

                    e.Graphics.DrawLine(_pen, _btn.ButtonBounds.Right - 1, _btn.ButtonBounds.Top, _btn.ButtonBounds.Right - 1, _btn.ButtonBounds.Bottom);

                    _alreadyPainted = true;
                } else if (_btn.DropDownButtonPressed) {
                    DrawingHelpers.FillRect(e.Graphics, _btn.ButtonBounds, ItemMouseOverBackColor);
                    DrawingHelpers.FillRect(e.Graphics, _btn.DropDownButtonBounds, ItemMouseDownBackColor);
                    DrawingHelpers.DrawSimpleBorder(e.Graphics, _rect, ItemBorderColor, ItemBorderSize);

                    DrawingHelpers.FillRect(e.Graphics, _btn.SplitterBounds, ItemBorderColor);
                    e.Graphics.DrawLine(_pen, _btn.ButtonBounds.Right - 1, _btn.ButtonBounds.Top, _btn.ButtonBounds.Right - 1, _btn.ButtonBounds.Bottom);

                    _alreadyPainted = true;
                }

                if (_btn.Selected) {
                    if (!_alreadyPainted) {
                        DrawingHelpers.FillRect(e.Graphics, _rect, ItemMouseOverBackColor);
                        DrawingHelpers.DrawSimpleBorder(e.Graphics, _rect, ItemBorderColor, ItemBorderSize);
                        DrawingHelpers.FillRect(e.Graphics, _btn.SplitterBounds, ItemBorderColor);
                        e.Graphics.DrawLine(_pen, _btn.ButtonBounds.Right - 1, _btn.ButtonBounds.Top, _btn.ButtonBounds.Right - 1, _btn.ButtonBounds.Bottom);
                    }
                }
            }
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e) {
            if (e.ToolStrip is ToolStripDropDownMenu _dropDownMenu) {
                using (var _brush = new SolidBrush(DroppedDownSurfaceBackColor))
                    e.Graphics.FillRectangle(_brush, e.ConnectedArea);

                using (var _pen = new Pen(DroppedDownSurfaceBorderColor)) {
                    if (e.ToolStrip.RightToLeft == RightToLeft.Yes) {
                        e.Graphics.DrawLines(_pen, new Point[] {
                            new Point(_dropDownMenu.Width - 1, 0),
                            new Point(_dropDownMenu.Width - 1, _dropDownMenu.Height - 1),
                            new Point(0, _dropDownMenu.Height - 1),
                            new Point(0, 0),
                            new Point(_dropDownMenu.Width - e.ConnectedArea.Width - 2, 0)
                        });
                    } else {
                        e.Graphics.DrawLines(_pen, new Point[] {
                            new Point(0, 0),
                            new Point(0, _dropDownMenu.Height - 1),
                            new Point(_dropDownMenu.Width - 1, _dropDownMenu.Height - 1),
                            new Point(_dropDownMenu.Width - 1, 0),
                            new Point(e.ConnectedArea.Width + 1, 0)
                        });
                    }
                }
            } else if (ToolStripBorderSides != BorderSides.None) {
                using (var _pen = new Pen(ToolStripBorderColor)) {
                    if (ToolStripBorderSides.HasFlag(BorderSides.Top))
                        e.Graphics.DrawLine(_pen, 0, 0, e.ToolStrip.Width - 1, 0);

                    if (ToolStripBorderSides.HasFlag(BorderSides.Left))
                        e.Graphics.DrawLine(_pen, 0, 0, 0, e.ToolStrip.Height - 1);
                    
                    if (ToolStripBorderSides.HasFlag(BorderSides.Bottom))
                        e.Graphics.DrawLine(_pen, 0, e.ToolStrip.Height - 1, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);

                    if (ToolStripBorderSides.HasFlag(BorderSides.Right))
                        e.Graphics.DrawLine(_pen, e.ToolStrip.Width - 1, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);
                }
            }
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e) {
            _itemImageMargin = e.AffectedBounds.Width;

            using (var _brush = new SolidBrush(ImageMarginBackColor))
                e.Graphics.FillRectangle(_brush, e.AffectedBounds);
        }











        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e) {
            if (UseCustomItemCheckImage)
                e.Graphics.DrawImage(ItemCheckImage, e.ImageRectangle);
            else
                base.OnRenderItemCheck(e);
        }

        protected override void OnRenderGrip(ToolStripGripRenderEventArgs e) {
            base.OnRenderGrip(e);
        }
    }
}
