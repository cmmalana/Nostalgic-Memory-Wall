# **Nostalgic Memory Wall**

Nostalgic Memory Wall is designed to work alongside the [Fotomoko](https://github.com/cmmalana/Fotomoko) program.  
It monitors the folder: **My Documents > Fotomoko2 > folder_name** where images captured by Fotomoko are stored.  
The program automatically takes the most recent files and displays them in a frame-like layout, similar to a hanging wall frame.

The frames are interactive — when touched, they glow for a few seconds.

## Image and Video Demo
<a href="https://www.facebook.com/share/v/1VFDZSQupz/">
  <img src="./actual%20usage.jpg" alt="Watch the demo" width="400">
</a>

## Features
- Automatically loads the most recent images from [Fotomoko](https://github.com/cmmalana/Fotomoko).
- Displays them in a “digital wall frame” style.
- Interactive touch frames that glow briefly when tapped.
- Simple controls:
  - **Q** → Hide cursor  
  - **W** → Show cursor

## Prerequisites
1. Ensure that both the iMultitouch device and the computer running Nostalgic Memory Wall are on the same computer.
2. Enable sharing for the folder path: **Documents > Fotomoko2 > folder_name**

## Setup
1. Connect the iMultitouch device and the computer with Nostalgic Memory Wall to the same network.  
2. Find the iMultitouch IP address using:
- Run `ipconfig` in Command Prompt.  
3. On the computer with Nostalgic Memory Wall, open File Explorer and enter: **\\\\ip address of the iMultitouch** (i.e. \\\\192.168.1.1)
4. Enter the iMultitouch username and password, then select **Save credentials**.
5. Run **Nostalgic Memory Wall** and enter the iMultitouch IP address. - If images appear in the frames, the setup is successful!
