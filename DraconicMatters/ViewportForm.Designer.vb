<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ViewportForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ViewportForm))
        Me.DrawTimer = New System.Windows.Forms.Timer(Me.components)
        Me.EngineContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EngineSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewportControl1 = New DraconicMatters.ViewportControl()
        Me.EngineContextMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'DrawTimer
        '
        Me.DrawTimer.Interval = 200
        '
        'EngineContextMenu
        '
        Me.EngineContextMenu.BackColor = System.Drawing.Color.White
        Me.EngineContextMenu.DropShadowEnabled = False
        Me.EngineContextMenu.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.EngineContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EngineSettingsToolStripMenuItem})
        Me.EngineContextMenu.Name = "EngineContextMenu"
        Me.EngineContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.EngineContextMenu.ShowImageMargin = False
        Me.EngineContextMenu.ShowItemToolTips = False
        Me.EngineContextMenu.Size = New System.Drawing.Size(232, 40)
        '
        'EngineSettingsToolStripMenuItem
        '
        Me.EngineSettingsToolStripMenuItem.Name = "EngineSettingsToolStripMenuItem"
        Me.EngineSettingsToolStripMenuItem.Size = New System.Drawing.Size(231, 36)
        Me.EngineSettingsToolStripMenuItem.Text = "Engine Settings"
        '
        'ViewportControl1
        '
        Me.ViewportControl1.ContextMenuStrip = Me.EngineContextMenu
        Me.ViewportControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ViewportControl1.Location = New System.Drawing.Point(0, 0)
        Me.ViewportControl1.Margin = New System.Windows.Forms.Padding(12, 12, 12, 12)
        Me.ViewportControl1.Name = "ViewportControl1"
        Me.ViewportControl1.Size = New System.Drawing.Size(1168, 1079)
        Me.ViewportControl1.TabIndex = 0
        '
        'ViewportForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(1168, 1079)
        Me.ContextMenuStrip = Me.EngineContextMenu
        Me.Controls.Add(Me.ViewportControl1)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.Name = "ViewportForm"
        Me.Text = "Draconic Matters"
        Me.EngineContextMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DrawTimer As Timer
    Friend WithEvents ViewportControl1 As ViewportControl
    Friend WithEvents EngineContextMenu As ContextMenuStrip
    Friend WithEvents EngineSettingsToolStripMenuItem As ToolStripMenuItem
End Class
