/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package example;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Graphics2D;
import java.awt.Image;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;
import javax.imageio.ImageIO;
import javax.swing.*;
import javax.swing.filechooser.FileNameExtensionFilter;

public final class Example extends JFrame{
    private final JFrame f;
    private JLabel jlab;
    private final JPanel jpan;
    private final JButton confirm;
    private final JButton cancel;
    private final JMenuBar menubar;
    private JMenu insert;
    private JMenu delete;
    private JMenuItem add_logo;
    private JMenuItem add_background;
    private JMenuItem det_logo;
    private JMenuItem det_background;
    private BufferedImage outputimage = null;
    private final String loadimage_path = "/Users/haijiewu/Desktop/image/";
    private final String logo_path = "/Users/haijiewu/Desktop/test/logo/out.jpg";
    private final String background_path = "/Users/haijiewu/Desktop/test/background/out.jpg";
    private final String addmessage = "picture has been added";
    private final String deletemessage = "picture has been deleted";
    private final int logo_width = 50;
    private final int logo_height = 50;
    private final int background_width = 200;
    private final int background_height = 200;
    
    
    public Example(){
        f = new JFrame("Killerhope museum");   
        f.setVisible(true);
        f.setSize(600,400);
        f.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        
        // panel for the display image
        jlab = new JLabel();
        jpan = new JPanel();
        jpan.add(jlab);
        f.add(jpan);
        
        // button to confirm images on the buttom
        confirm = new JButton("Confirm");
        cancel = new JButton("Cancel");
        JPanel jpan_buttom = new JPanel();
        jpan_buttom.setBackground(Color.red);
        jpan_buttom.add(confirm);
        jpan_buttom.add(cancel);
        f.add(jpan_buttom,BorderLayout.SOUTH);
        
       
        menubar = new JMenuBar();
        emenubar_insert();
        emenubar_delete();
        f.setJMenuBar(menubar);
    }
    
    public void emenubar_insert(){
        insert = new JMenu("INSERT");
        menubar.add(insert);
        add_logo = new JMenuItem("LOGO");
        insert.add(add_logo); 
        add_background = new JMenuItem("BACKGROUND");
        insert.add(add_background);
        
        add_logo.addActionListener(new ActionListener(){
            @Override
            public void actionPerformed(ActionEvent e){
                JFileChooser fc = new JFileChooser();
                fc.setCurrentDirectory(new java.io.File(loadimage_path));
                fc.setDialogTitle("Select Image");
                // set the filter to be jpg and jpeg
                FileNameExtensionFilter format = new FileNameExtensionFilter("JPEG/JPG","jpg","jpeg");
                fc.setFileFilter(format);               
                if(fc.showOpenDialog(null) == JFileChooser.APPROVE_OPTION){  
                    final java.io.File f = fc.getSelectedFile();
                    resizeimage(f,logo_width,logo_height);
                    displayimage(outputimage);
                    //jlab.setHorizontalAlignment(JLabel.CENTER);
                    validate();
                    //System.out.println(f);
                    writeimage(logo_path);
                    confirmaddact(f,addmessage,true);
                    canceladdact();
                }
            }
        });
        
        add_background.addActionListener(new ActionListener(){
            @Override
            public void actionPerformed(ActionEvent e){
                JFileChooser fc = new JFileChooser();
                fc.setCurrentDirectory(new java.io.File(loadimage_path));
                fc.setDialogTitle("Select Image");
                // set the filter to be jpg and jpeg
                FileNameExtensionFilter format = new FileNameExtensionFilter("JPEG/JPG","jpg","jpeg");
                fc.setFileFilter(format);               
                if(fc.showOpenDialog(null) == JFileChooser.APPROVE_OPTION){  
                    final java.io.File f = fc.getSelectedFile();
                    resizeimage(f,background_width,background_height);
                    displayimage(outputimage);
                    //jlab.setHorizontalAlignment(JLabel.CENTER);
                    validate();
                    //System.out.println(f);
                    writeimage(background_path);
                    confirmaddact(f,addmessage,true);
                    canceladdact();
                }
            }
        });
    }
    
    public void emenubar_delete(){
        delete = new JMenu("DELETE");
        menubar.add(delete);
        det_logo = new JMenuItem("LOGO");
        delete.add(det_logo);
        det_background = new JMenuItem("BACKGROUND");
        delete.add(det_background);
        
        det_logo.addActionListener(new ActionListener(){
            @Override
            public void actionPerformed(ActionEvent e){
                try {
                    File file = new File(logo_path);                    
                    Image original_image = ImageIO.read(file);
                    displayimage(original_image);
                    confirmaddact(file,deletemessage,false);
                } catch (IOException ex) {
                    JOptionPane.showMessageDialog(null, "no image in the logo folder !!");
                }
            }
        });
        
        det_background.addActionListener(new ActionListener(){
            @Override
            public void actionPerformed(ActionEvent e){
                try {
                    File file = new File(background_path);                    
                    Image original_image = ImageIO.read(file);
                    displayimage(original_image);
                    confirmaddact(file,deletemessage,false);
                } catch (IOException ex) {
                    JOptionPane.showMessageDialog(null, "no image in the logo folder !!");
                }
            }
        });
    }
    
            
    public void writeimage(String path){
        try {
            ImageIO.write(outputimage,"jpg",new File(path));
        } catch (IOException ex) {
            JOptionPane.showMessageDialog(null, "no such directory !!");
        }
    }        
            
    public void removeimage(){
        jpan.remove(jlab);
        jpan.revalidate();
        jpan.repaint();
        jlab = new JLabel();
        jpan.add(jlab);
    }

    public void displayimage(Image a){
        jlab.setIcon(new ImageIcon(a));
    }
    
    public void confirmaddact(final File a,final String message, final Boolean write){
        confirm.addActionListener(new ActionListener(){     
        @Override
            public void actionPerformed(ActionEvent e){
                JOptionPane.showMessageDialog(null, message);
                if (write == false){
                    a.delete();
                }
                //
                removeimage();
                confirm.removeActionListener(this);
            }           
        });       
    }
    
    public void canceladdact(){
        cancel.addActionListener(new ActionListener(){     
        @Override
            public void actionPerformed(ActionEvent a){    
                removeimage();
                for(ActionListener a1 : confirm.getActionListeners()){
                    confirm.removeActionListener(a1);
                }
            }           
        });  
    }
    
    public void resizeimage(File image, int width, int height){
        try {
            BufferedImage inputimage = ImageIO.read(image);
            outputimage = new BufferedImage(width,height,inputimage.getType());
            Graphics2D g2d = outputimage.createGraphics();
            g2d.drawImage(inputimage,0,0,width,height,null);
            g2d.dispose();
        } catch (IOException ex) {
            
        } 
    }
    
    public static void main(String[] args){
        try{
            Thread.sleep(3000);
        }
        catch(Exception e){
            
        }
        new Example();//
    }
}

