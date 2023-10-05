namespace Project2;

public partial class MainPage : ContentPage
{
    List<Button> numButtons;
    List<Button> letButtons;

	public MainPage()
	{
		InitializeComponent();

        numButtons = new List<Button>();
        letButtons = new List<Button>();

        // Setting up buttons in a way that should reduce calling names from XAML
        int count = 0;
        int letterCount = 65;

        // Creating buttons for number pad and adding them to xaml grid
        // 0-9, then A-F
        for (int i = 1; i < 5; i++)
        {

            for (int j = 0; j < 4; j++)
            {

                // If adding letter buttons
                if ( (i >= 3 && j >= 2) || i==4)
                {
                    char letter = (char)letterCount;
                    Button btn = new Button {Text = letter.ToString()};
                    btn.Clicked += Button_Clicked;
                    grid.SetColumn(btn, j);
                    grid.SetRow(btn, i);
                    grid.Add(btn);
                    letButtons.Add(btn);
                    letterCount++;

                // If adding numeric buttons
                } else {
                    Button btn = new Button {Text = count.ToString()};
                    btn.Clicked += Button_Clicked;
                    grid.SetColumn(btn, j);
                    grid.SetRow(btn, i);
                    grid.Add(btn);
                    numButtons.Add(btn);
                    count++;
                }

            }
        }

        // Setting default mode to convert to decimal
        pick.SelectedIndex = 2;
        pick_SelectedIndexChanged(null, null);

    }


    /// <summary>
    /// When any of the numeric or alphabetical buttons are clicked
    /// </summary>
    /// <param name="sender"></param> This represents the button that is clocked
    /// <param name="e"></param>
    private void Button_Clicked(object sender, EventArgs e)
    {
        // Adding button text to label above, no leading zeros allowed
        if ( lab.Text.Equals("0") ) {
            lab.Text = (sender as Button).Text;
        } else {
            lab.Text += (sender as Button).Text;
        }

        convertNum(lab.Text);
    }

    /// <summary>
    /// Converting the entry number to the other types (hexadecimal, binary, decimal, octal)
    /// </summary>
    /// <param name="num"></param> the number that the user has created in the entry
    private void convertNum(string num)
    {
        int dec;

        if (pick.SelectedIndex == 0) {
            // Converting bin to dec
            dec = Convert.ToInt32(num, 2);
        } else if (pick.SelectedIndex == 1) {
            // Converting oct to dec
            dec = Convert.ToInt32(num, 8);
        } else if (pick.SelectedIndex == 3) {
            // Converting hex to dec
            dec = Convert.ToInt32(num, 16);
        } else {
            dec = int.Parse(num) ;
        }

        // Converting decimal values to other types
        binNum.Text = Convert.ToString(dec, 2);
        octNum.Text = Convert.ToString(dec, 8);
        hexNum.Text = Convert.ToString(dec, 16);
        decNum.Text = dec.ToString();

    }

    /// <summary>
    /// The method called when the delete button is clicked, the latest label digit is removed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void delButton_Clicked(object sender, EventArgs e)
    {
        // If the entry only has 1 value and delete is pressed then we label reset it to 0, otherwise remove last digit
        if (! (lab.Text.Length == 1) ) {
            lab.Text = lab.Text.Remove(lab.Text.Length - 1);
            convertNum(lab.Text);
        } else if (lab.Text.Length == 1)
        {
            lab.Text = "0";
            convertNum(lab.Text);
        }
    }

    /// <summary>
    /// The method called when the clear button is clicked, we just reset the label to 0
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void clrButton_Clicked(object sender, EventArgs e)
    {
        lab.Text = "0";
        convertNum(lab.Text);
    }

    private void pick_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Zeroing the label when input mode is switched
        clrButton_Clicked(null, null);

        // Enabling all buttons, so we can just disable the ones we don't need
        foreach (Button btn in numButtons)
        {
            btn.IsEnabled = true;
        }
        foreach (Button btn in letButtons)
        {
            btn.IsEnabled = true;
        }

        // Disable non-binary buttons
        if (pick.SelectedIndex == 0)
        {
            
            for (int i = 2; i < numButtons.Count; i++)
            {
                numButtons.ElementAt(i).IsEnabled = false;
            }
            for (int i = 0; i < letButtons.Count; i++)
            {
                letButtons.ElementAt(i).IsEnabled = false;
            }
        }
        // Disable non-octal buttons
        else if (pick.SelectedIndex == 1)
        {
            for (int i = 8; i <= 9; i++)
            {
                numButtons.ElementAt(i).IsEnabled = false;
            }
            for (int i = 0; i < letButtons.Count; i++)
            {
                letButtons.ElementAt(i).IsEnabled = false;
            }
        }
        // Disable non-decimal buttons
        else if (pick.SelectedIndex == 2)
        {
            for (int i = 0; i < letButtons.Count; i++)
            {
                letButtons.ElementAt(i).IsEnabled = false;
            }
        }
        // Don't have to disable non-hexadecimal buttons, because those don't exist
    }


}

