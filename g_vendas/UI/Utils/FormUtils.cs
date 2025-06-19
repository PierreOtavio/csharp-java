using System;
using System.Drawing;
using System.Windows.Forms;

public static class FormUtils
{
    public static void SetIcon(Form form, string iconPath)
    {
        try
        {
            form.Icon = new Icon(iconPath);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao carregar ícone: {ex.Message}");
        }
    }
}
