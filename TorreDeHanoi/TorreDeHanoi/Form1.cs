using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TorreDeHanoi
{
    public partial class Form1 : Form
    {
        private int movimientos;
        private Stack<Control>[] torres;
        private int torreSeleccionada = -1;
        private bool juegoCompletado = false;
        public Form1()
        {
            InitializeComponent();
            InicializarTorres();
            button5.Click += button5_Click;
        }

        private void InicializarTorres()
        {
            // Remover los botones de los paneles
            foreach (var panel in new Panel[] { panel1, panel2, panel3 })
            {
                panel.Controls.Clear();
            }

            // Agregar los botones al Panel 1
            panel1.Controls.AddRange(new Control[] { button1, button2, button3, button4 });

            // Reiniciar la pila de torres
            torres = new Stack<Control>[3];
            torres[0] = new Stack<Control>();
            torres[1] = new Stack<Control>();
            torres[2] = new Stack<Control>();

            // Agregar los botones a la primera torre (panel1)
            torres[0].Push(button1);
            torres[0].Push(button2);
            torres[0].Push(button3);
            torres[0].Push(button4);

            // Reiniciar contador de movimientos
            movimientos = 0;
            lblMovimientos.Text = "Movimientos: 0";

            // Reiniciar estado del juego completado
            juegoCompletado = false;
        }

        private void panel_Click(object sender, EventArgs e)
        {
            Panel panel = sender as Panel;
            int torre = int.Parse(panel.Tag.ToString());

            if (torreSeleccionada == -1)
            {
                torreSeleccionada = torre;
            }
            else
            {
                MoverDisco(torreSeleccionada, torre);
                torreSeleccionada = -1;
            }
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            InicializarTorres();
        }

        private void MoverDisco(int origen, int destino)
        {
            // Verificar si el juego ya se ha completado
            if (juegoCompletado)
            {
                // Restablecer el estado del juego
                InicializarTorres();
                juegoCompletado = false;
                return;
            }
            if (torres[origen].Count == 0 || (torres[destino].Count > 0 && torres[origen].Peek().Width > torres[destino].Peek().Width))
            {
                MessageBox.Show("Movimiento inválido");
                return;
            }

            Control disco = torres[origen].Pop();
            torres[destino].Push(disco);

            // Actualizar la posición del disco
            switch (destino)
            {
                case 0:
                    disco.Parent = panel1;
                    break;
                case 1:
                    disco.Parent = panel2;
                    break;
                case 2:
                    disco.Parent = panel3;
                    break;
            }

            disco.BringToFront();

            movimientos++;
            lblMovimientos.Text = "Movimientos: " + movimientos;
            // Verificar si el juego ha sido completado
            if (torres[2].Count == 4) // Cambia 4 por el número total de botones
            {
                MessageBox.Show("¡Felicidades! Has completado el juego.");
                juegoCompletado = true;
            }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            panel_Click(sender, e);
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            panel_Click(sender, e);
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            panel_Click(sender, e);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Este método puede permanecer vacío si no necesitas pintar nada específico en el panel.
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InicializarTorres();
        }
    }
}