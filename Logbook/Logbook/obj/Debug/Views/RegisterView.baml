   M S B A M L   `   `   `  ����B  >Logbook, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Logbook.ViewsLogbook.Views  5   	        Logbook.Views.RegisterView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Logbook.Views     5   	     � 	Resources  5	       �� 
TargetType �� Property �� Value-  'System.Windows.Input.KeyboardNavigation �� 
TargetType ��Name �� CornerRadius   WindowBackgroundBrush J� OpacityMask ��Name	 J� SnapsToDevicePixels   ScrollViewerControlTemplate1  MyButtonStyle
 �� Padding �� Triggers P� Property P� Value   BooleanToVisibilityConverter��%c  'G�       &	 S    &	 2    �� 5
   
   " G�6   .�5      ��  �@G�PasswordChar$ *��6-   �� 5       �@ TabNavigation$
 None=�6A   �� 5       � ��$ 1q�60   �� 5       � ��$
 Left=�6;   �� 5       � ��$ 1q�6(   �� 5       �@�FocusVisualStyle 61   Z� �� 5       �@J�	AllowDrop . 6*   �� 5       � �� 5      �� 5      " G�6&   .R�5       �� $ Bd��63   $ 10��6!   #���   6?   ���#e5e5e5$��1q�6�    6�   Z� ._�5"      �� $ PART_ContentHost��6g   #	 z	 6+   #���  6�   5$      5(      5,      5.      �� 52   
   " ��6&   .�53      ��  �@�OverridesDefaultStyle . 66   �� 54       � ��$ 5q�6'   �� 55       � �� 56      �� 57      " ��6&   .R�59      �� $��border��6!   $��1q�5:      $	
 4,2q�5;      $ 10��5<      #��z��5=      ._�5>      �� $��Center=�6/   $��Center=�6L   5?       5A      P� 5B       �@J�IsMouseOver . . �5C   "   ��  � �� �#ed939d6?   5D      5G      5I      5J      �� 5K   
   6'   5L      .��5M      �  � ColumnDefinitions 5N   
   �� 5O      $
��0.6* �6   6.   �� 5P      $
��0.4* �6   6.   ��5R   
   �� 5S       �� ImageSource$ image 1.png��6   .+�5V   
   �� $��1��6   ���#1A1918.+�5W      �  5X      �� 5Y      $
��0.3* �6'   66   �� 5Z      $
��0.3* �6'   66   �� 5[      $
��0.3* �6'   66    � RowDefinitions 5]      �� 5^      $
��0.3* �6$   64   �� 5_      $
��0.3* �6$   64   �� 5`      $
��0.3* �6$   64   .+�5c      �� $��	0,200,0,0q�6   $��1��60   .+�5d      �� 
�������$��28(�66   .��6D   
Войти6K   �� 5f      
�������$��14(�66   $��5,20,5,5q�6D   .��6V   
Логин6]   �� 5g      
������ $��5q�6O   $��18(�6[   ��6   �� *Login+.��5h        5i      ��%_   '��       �� 5j      " ��6$   .�5k   "   ��  �@��CornerRadius$ 10��6A   5l      5m      .��6-   5n      �� 5p      
�������$��14(�66   $��5,20,5,5q�6D   .��6V   Имя6[   �� 5q      
������ $��5q�6N   $��18(�6Z   ��6   �� *Name+.��5r        5s      ��%_   '��       �� 5t      " ��6$   .�5u   "   ��  �@��CornerRadius$ 10��6A   5v      5w      .��6-   5x      �� 5{      
�������$��14(�66   $��5,20,5,5q�6D   .��6V   Пароль6^   �� 5|      
������ $��5q�6R   $��18(�6^   ��6   �� *
Password+.��5}        5~      ��%_   '��       �� 5      " ��6$   .�5�   "   ��  �@��CornerRadius$ 10��6A   5�      5�      .��6-   5�      �� 5�      
�������$��14(�66   $��5,20,5,5q�6D   .��6V   Повтор пароля6e   �� 5�      
������ $��5q�6W   $��18(�6c   ��6   �� *PasswordAgain+.��5�        5�      ��%_   '��       �� 5�      " ��6$   .�5�   "   ��  �@��CornerRadius$ 10��6A   5�      5�      .��6-   5�      �� 5�      $��5q�6   $��Регистрация��6(   $��Stretch=�6>   #��[ 6\   ���#d4a752$	��150��5�   J   $��18(�6Y   $��10q�6g   ��5�      �� *RegisterCommand+��5�      ��  �� ElementName$ PasswordBox��6g   �� 5�       �� Orientation$ 
Horizontal=�6!   $��5q�6:   .+�5�      �� $��16(�6    
�������.��6H   Имеете аккаунт?6Y   �� 5�      $��
Войти��6!   $
��Left=�61   #��[ 6L   $��0q�6s   $��0q�5�   !   ���#1A1918
�������$��16(�5�   m   ��6   �� *LoginCommand+6m   5�      �� 5�      $8��2Проверьте введенные данные��6   
�������y�5�      �� *ErrorVisibility+ �� 	Converter# [ 6~   5�      5�      5�      �� 5�   
   $��0��6   .+�5�      �  5�      �� 5�      $
��0.3* �6'   66   �� 5�      $
��0.6* �6'   66   �� 5�      $
��0.3* �6'   66    5�      �� 5�      $
��0.3* �6$   64   �� 5�      $
��0.6* �6$   64   �� 5�      $
��0.3* �6$   64   .+�5�      �� $��1��6   $��1��6*   $��	0,200,0,0q�6:   .+�5�      �� 
�������$��28(�66   .��6D   )'Электронный формуляр6Z   �� 5�      
�������$��28(�66   .��6D   библиотеки6P   �� 5�      
�������$��16(�66   $��0,20,0,0q�6D   .��6V   ECУдобное приложение для выбора книги,5�      �� 5�      
�������$��16(�66   .��6D   KIкоторая придется Вам по душе. Вы сможете5�      �� 5�      
�������$��16(�66   .��6D   -+отлично провести время.5�      �� 5�      
�������$��16(�66   .��6D   ><Данное приложение было создано в5�      �� 5�      
�������$��16(�66   .��6D   75качестве проэкта по предмету5�      �� 5�      
�������$��16(�66   .��6D   0."объекто-ориентированное5�      �� 5�      
�������$��16(�66   .��6D   /-программирование" в БГТУ5�      5�      5�      5�      5�      5�      