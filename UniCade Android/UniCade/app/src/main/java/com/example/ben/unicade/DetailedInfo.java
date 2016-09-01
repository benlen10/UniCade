package com.example.ben.unicade;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.drawable.BitmapDrawable;
import android.media.Image;
import android.net.Uri;
import android.os.Bundle;
import android.os.Environment;
import android.os.StrictMode;
import android.provider.MediaStore;
import android.text.InputType;
import android.view.Menu;
import android.view.View;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;
import android.support.v7.app.ActionBarActivity;
import android.support.v7.widget.PopupMenu;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;


/**
 * Created by Ben on 12/20/2015.
 */
public class DetailedInfo extends Activity{

    public static TextView t5;
    public static TextView t9;
    public static TextView t10;
    public static TextView t11;  //Title
    public static TextView t12;
    public static TextView t13;
    public static TextView t14;
    public static TextView t15;
    public static TextView t16;
    public static TextView t17;
    public static Button b4;
    public static Button b17;
    public static Button b22;
    public static ImageView i2;
    public static ImageView i3;
    public static ImageView i4;
    public static ImageView i5;
    public static ImageView i6;
    public static ImageView i8;
    public static ImageView i11;
    public static CheckBox c1;
    private String resultText = "";
    private String origText = "";
    private String popupTitle = "";
    public static Game curGame;
    final Context context = this;
    private boolean boxfrontFull = false;
    private boolean boxbackFull = false;
    private boolean screenshotFull = false;
    private boolean consoleFull = false;
    private android.view.ViewGroup.LayoutParams origParams;
    private String imgDecodableString;
    private static int RESULT_LOAD_IMG = 1;
    private static String viewStatus;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.info_activity);
        if (android.os.Build.VERSION.SDK_INT > 9) {
            StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
            StrictMode.setThreadPolicy(policy);
        }
        findView();
        if(SettingsWindow.displayConImage==1) {
            i6.setImageResource(MainActivity.getImageId(context, MainActivity.conImage));
        }
        i8.setImageResource(R.drawable.splash_image);
        loadInfo(MainActivity.curGame);
    }

    public void closeInfoWindow(View v){
        super.onBackPressed();
    }

    public void refreshGameInfoButton(View v){
        loadInfo(curGame);
    }

    public void rescrapeSingleGame(View v){
        WebOps.scrapeInfo(curGame);
        Toast.makeText(this, "Operation Complete",
                Toast.LENGTH_LONG).show();
        loadInfo(curGame);
    }

    public void rescrapeConsole(View v){
        Console curConsole = null;

        int i = 0;
        for(Console c : MainActivity.dat.consoleList){
            if(c.getName().equals(curGame.getConsole())){
                curConsole = c;
                break;
            }
        }
        if(curConsole==null){
            Toast.makeText(this, "Operation Failed. (Could Not Find Console)",
                    Toast.LENGTH_LONG).show();
            return;
        }
        for(Game g : curConsole.getGameList()) {
            WebOps.scrapeInfo(g);
            String text = ("Game " + Integer.toString(i) + "of" + Integer.toString(MainActivity.curConsole.gameCount));
            //Toast.makeText(this, text,Toast.LENGTH_LONG).show();
        }
        WebOps.scrapeInfo(curGame);
        Toast.makeText(this, "Operation Complete",
                Toast.LENGTH_LONG).show();
        loadInfo(curGame);
    }

    public void saveDatabase(View v){
        FileOps.saveDatabase("Database.txt");
    }

    public void loadInfo(Game g){
        if(g==null){
            return;
        }
        curGame = g;
        t5.setText(("Title: " + g.getTitle()));
        t9.setText(("Console: "+ g.getConsole()));
        t10.setText(("Release Date: " + g.getReleaseDate()));
        t11.setText(("Publisher: " + g.getPublisher()));
        t12.setText(("Critic Score: " + g.getCriticScore()));
        t13.setText(("Players: " + g.getPlayers()));
        t14.setText(("ESRB Rating: " + g.getEsrb()));
        t15.setText(("ESRB Descriptors: " + g.getEsrbDescriptor()));
        t16.setText(("Launch Count: " + g.launchCount));
        t17.setText(("Description: " + g.getDescription()));
        if(g.getFav()>0) {
            c1.setChecked(true);
        }

        Bitmap bitmap = BitmapFactory.decodeFile(Environment.getExternalStorageDirectory().getAbsolutePath()+ "/Unicade/Media/" + g.getConsole().toLowerCase().replace(" ", "") + "_" + g.getTitle().toLowerCase().replace(" ", "") + "_boxfront"+".png");
        i2.setImageBitmap(bitmap);
         bitmap = BitmapFactory.decodeFile(Environment.getExternalStorageDirectory().getAbsolutePath()+ "/Unicade/Media/" + g.getConsole().toLowerCase().replace(" ", "") + "_" + g.getTitle().toLowerCase().replace(" ", "") + "_boxback"+".png");
        i3.setImageBitmap(bitmap);
         bitmap = BitmapFactory.decodeFile(Environment.getExternalStorageDirectory().getAbsolutePath()+ "/Unicade/Media/" + g.getConsole().toLowerCase().replace(" ", "") + "_" + g.getTitle().toLowerCase().replace(" ", "") + "_screenshot"+".png");
        i4.setImageBitmap(bitmap);


        if (SettingsWindow.displayESRBLogo==1) {
            if (g.getEsrb().equals("Everyone")) {
                i5.setImageResource(R.drawable.everyone);
            } else if (g.getEsrb().equals("Everyone 10+")) {
                i5.setImageResource(R.drawable.everyone10);
            } else if (g.getEsrb().equals("Teen")) {
                i5.setImageResource(R.drawable.teen);
            } else if (g.getEsrb().equals("Mature")) {
                i5.setImageResource(R.drawable.mature);
            } else if (g.getEsrb().equals("Adults Only (AO)")) {
                i5.setImageResource(R.drawable.ao);
                ;
            } else {
                i5.setImageResource(0);
            }
        }
    }

    public void findView(){
        t5 = (TextView) findViewById(R.id.textView5);
        t9 = (TextView) findViewById(R.id.textView9);
        t10 = (TextView) findViewById(R.id.textView10);
        t11 = (TextView) findViewById(R.id.textView11);
        t12 = (TextView) findViewById(R.id.textView12);
        t13 = (TextView) findViewById(R.id.textView13);
        t14 = (TextView) findViewById(R.id.textView14);
        t15 = (TextView) findViewById(R.id.textView15);
        t16 = (TextView) findViewById(R.id.textView16);
        t17 = (TextView) findViewById(R.id.textView17);
        i2 = (ImageView) findViewById(R.id.imageView2);
        i3 = (ImageView) findViewById(R.id.imageView3);
        i4 = (ImageView) findViewById(R.id.imageView4);
        i5 = (ImageView) findViewById(R.id.imageView5);
        i6 = (ImageView) findViewById(R.id.imageView6);
        i8 = (ImageView) findViewById(R.id.imageView8);
        i11 = (ImageView) findViewById(R.id.imageView11);
        b4 = (Button) findViewById(R.id.button4);
        b17 = (Button) findViewById(R.id.button17);
        b22 = (Button) findViewById(R.id.button22);
        c1 = (CheckBox) findViewById(R.id.checkBox);
        b17.setVisibility(View.INVISIBLE);
        i2.bringToFront();
        i3.bringToFront();
        i4.bringToFront();

        b22.setVisibility(View.INVISIBLE);



    }



    public void showPopup(String title, String message){
        AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(
                context);

        // set title
        alertDialogBuilder.setTitle(title);
        alertDialogBuilder
                .setMessage(message)
                .setCancelable(false)
                .setPositiveButton("OK", new DialogInterface.OnClickListener() {
                            public void onClick(DialogInterface dialog, int id) {
                                dialog.cancel();
                            }
                        }
                );
        AlertDialog alertDialog = alertDialogBuilder.create();
        alertDialog.show();
    }

    public void boxfrontFullscreen(View v){
        if(boxfrontFull) {
            android.view.ViewGroup.LayoutParams params = i2.getLayoutParams();
            params.width = 500;
            params.height = 500;
            i2.setLayoutParams(params);
            b17.setVisibility(View.INVISIBLE);
            b22.setVisibility(View.INVISIBLE);
            viewStatus = " ";
            boxfrontFull = false;
        }
        else{

            android.view.ViewGroup.LayoutParams params = i2.getLayoutParams();
            params.width = 1500;
            params.height = 1500;
            i2.setLayoutParams(params);
            b22.setText("Set BoxFront");
            b17.setVisibility(View.VISIBLE);
            b22.setVisibility(View.VISIBLE);
            viewStatus = "boxfront";
            boxfrontFull = true;
        }
    }

    public void boxbackFullscreen(View v){
        if(boxbackFull) {
            android.view.ViewGroup.LayoutParams params = i3.getLayoutParams();
            params.width = 500;
            params.height = 500;
            i3.setLayoutParams(params);
            b17.setVisibility(View.INVISIBLE);
            b22.setVisibility(View.INVISIBLE);
            viewStatus = "boxback";
            boxbackFull = false;
        }
        else{

            android.view.ViewGroup.LayoutParams params = i3.getLayoutParams();
            params.width = 1500;
            params.height = 1500;
            i3.setLayoutParams(params);
            b22.setText("Set BoxBack");
            b17.setVisibility(View.VISIBLE);
            b22.setVisibility(View.VISIBLE);
            boxbackFull = true;
        }
    }

    public void screenshotFullscreen(View v){
        if(screenshotFull) {
            android.view.ViewGroup.LayoutParams params = i4.getLayoutParams();
            params.width = 500;
            params.height = 500;
            i4.setLayoutParams(params);
            b17.setVisibility(View.INVISIBLE);
            b22.setVisibility(View.INVISIBLE);
            viewStatus = " ";
            screenshotFull = false;
        }
        else{

            android.view.ViewGroup.LayoutParams params = i4.getLayoutParams();
            params.width = 1500;
            params.height = 1500;
            i4.setLayoutParams(params);
            b22.setText("Set Screenshot");
            b17.setVisibility(View.VISIBLE);
            b22.setVisibility(View.VISIBLE);
            viewStatus = "screenshot";
            screenshotFull = true;
        }
    }

    public void consoleFullscreen(View v){
        if(consoleFull) {
            android.view.ViewGroup.LayoutParams params = i4.getLayoutParams();
            params.width = 500;
            params.height = 500;
            i6.setLayoutParams(params);
            consoleFull = false;
        }
        else{

            android.view.ViewGroup.LayoutParams params = i4.getLayoutParams();
            params.width = 1500;
            params.height = 1500;
            i6.setLayoutParams(params);
            consoleFull = true;
        }
    }

    public void toggleFav(View v){
        if(c1.isChecked()){
            curGame.setFav(1);
            System.err.println("IS FAV");
        }
        else
        {
            curGame.setFav(0);
            System.err.println("IS NOT FAV");
        }
    }



    public void editRelease(View v){
        PromptDialog dlg = new PromptDialog(DetailedInfo.this, R.string.ok, R.string.cancel) {
            @Override
            public boolean onOkClicked(String input) {
                curGame.setReleaseDate(input);
                loadInfo(curGame);
                return true;

            }
        };
        dlg.show();

    }

    public void editPublisher(View v){
        PromptDialog dlg = new PromptDialog(DetailedInfo.this, R.string.ok, R.string.cancel) {
            @Override
            public boolean onOkClicked(String input) {
                curGame.setPublisher(input);
                loadInfo(curGame);
                return true;

            }
        };
        dlg.show();
    }

    public void editScore(View v){
        PromptDialog dlg = new PromptDialog(DetailedInfo.this, R.string.ok, R.string.cancel) {
            @Override
            public boolean onOkClicked(String input) {
                curGame.setCriticScore(input);
                loadInfo(curGame);
                return true;

            }
        };
        dlg.show();
    }

    public void editPlayers(View v){
        PromptDialog dlg = new PromptDialog(DetailedInfo.this, R.string.ok, R.string.cancel) {
            @Override
            public boolean onOkClicked(String input) {
                curGame.setPlayers(input);
                loadInfo(curGame);
                return true;

            }
        };
        dlg.show();
    }

    public void editEsrb(View v){
        PromptDialog dlg = new PromptDialog(DetailedInfo.this, R.string.ok, R.string.cancel) {
            @Override
            public boolean onOkClicked(String input) {
                curGame.setEsrb(input);
                loadInfo(curGame);
                return true;

            }
        };
        dlg.show();
    }

    public void editEsrbDescriptors(View v){
        PromptDialog dlg = new PromptDialog(DetailedInfo.this, R.string.ok, R.string.cancel) {
            @Override
            public boolean onOkClicked(String input) {
                curGame.setEsrbDescriptors(input);
                loadInfo(curGame);
                return true;

            }
        };
        dlg.show();
    }

    public void editLaunchCount(View v){
        PromptDialog dlg = new PromptDialog(DetailedInfo.this, R.string.ok, R.string.cancel) {
            @Override
            public boolean onOkClicked(String input) {
                curGame.launchCount = safeParse(input);
                loadInfo(curGame);
                return true;

            }
        };
        dlg.show();
    }

    public void editDescription(View v){
        PromptDialog dlg = new PromptDialog(DetailedInfo.this, R.string.ok, R.string.cancel) {
            @Override
            public boolean onOkClicked(String input) {
                curGame.setDescription(input);
                loadInfo(curGame);
                return true;

            }
        };
        dlg.show();
    }



    public void loadImagefromGallery(View view) {
        // Create intent to Open Image applications like Gallery, Google Photos
        Intent galleryIntent = new Intent(Intent.ACTION_PICK,
                android.provider.MediaStore.Images.Media.EXTERNAL_CONTENT_URI);
        // Start the Intent
        startActivityForResult(galleryIntent, RESULT_LOAD_IMG);
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        try {
            // When an Image is picked
            if (requestCode == RESULT_LOAD_IMG && resultCode == RESULT_OK
                    && null != data) {
                // Get the Image from data

                Uri selectedImage = data.getData();
                String[] filePathColumn = { MediaStore.Images.Media.DATA };

                // Get the cursor
                Cursor cursor = getContentResolver().query(selectedImage,
                        filePathColumn, null, null, null);
                // Move to first row
                cursor.moveToFirst();

                int columnIndex = cursor.getColumnIndex(filePathColumn[0]);
                imgDecodableString = cursor.getString(columnIndex);
                cursor.close();
                // Set the Image in ImageView after decoding the String
                i11.setVisibility(View.INVISIBLE);
                i11.setImageBitmap(BitmapFactory
                        .decodeFile(imgDecodableString));
                //Save image
                Bitmap bm=((BitmapDrawable)i11.getDrawable()).getBitmap();
                FileOutputStream out = null;
                File sdCard = Environment.getExternalStorageDirectory();
                String filename = (sdCard.getAbsolutePath() + "/UniCade/Media/" + curGame.getConsole().toLowerCase().replace(" ", "") + "_" + curGame.getTitle().toLowerCase().replace(" ", "") + "_"+ viewStatus+".png");
                System.err.println("SAVE IMAGE: " + filename);
                try {
                    out = new FileOutputStream(filename);
                    bm.compress(Bitmap.CompressFormat.PNG, 100, out);
                    loadInfo(curGame);
                } catch (FileNotFoundException e) {
                    e.printStackTrace();
                }

            } else {
                Toast.makeText(this, "No Image Selected",
                        Toast.LENGTH_LONG).show();
            }
        } catch (Exception e) {
            Toast.makeText(this, "Unknown Error", Toast.LENGTH_LONG)
                    .show();
        }

    }

    /*public void showPopupMenu(View view){
        PopupMenu popupMenu = new PopupMenu(DetailedInfo.this, view);
        popupMenu.setOnMenuItemClickListener(DetailedInfo.this);
        popupMenu.inflate(R.menu.popup_menu);
        popupMenu.show();
        }*/

    public void deleteImage(View v){
        File file = new File((Environment.getExternalStorageDirectory().getAbsolutePath() + "/UniCade/Media/" + curGame.getConsole().toLowerCase().replace(" ", "") + "_" + curGame.getTitle().toLowerCase().replace(" ", "") + "_"+ viewStatus+".png"));
        System.err.println("DELETE IMAGE: "+ (Environment.getExternalStorageDirectory().getAbsolutePath() + "/UniCade/Media/" + curGame.getConsole().toLowerCase().replace(" ", "") + "_" + curGame.getTitle().toLowerCase().replace(" ", "") + "_"+ viewStatus+".png"));
        if (file.exists()) {
        if(file.delete()){
            Toast.makeText(this, "Image Deleted",
                    Toast.LENGTH_LONG).show();
        }
            else{
            Toast.makeText(this, "Error Deleting",
                    Toast.LENGTH_LONG).show();
        }

            loadInfo(curGame);
        }
        else{
            Toast.makeText(this, "Image does not exist",
                    Toast.LENGTH_LONG).show();
        }
    }

    public void showInputDialog(){
        PromptDialog dlg = new PromptDialog(DetailedInfo.this, R.string.ok, R.string.cancel) {
            @Override
            public boolean onOkClicked(String input) {
                resultText = input;
                return true;

            }
        };
        dlg.show();


    }

    public static int safeParse(String text) {
        try {
            return safeParse(text);
        } catch (NumberFormatException e) {
            return 0;
        }
    }

}
