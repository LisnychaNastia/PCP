with Ada.Text_IO; use Ada.Text_IO;

procedure Main is

   can_stop : boolean := false;
   pragma Atomic(can_stop);

   task type break_thread;
   task type main_thread(id, step : Long_Long_Integer);

   task body break_thread is
   begin
      delay 5.0;
      can_stop := true;
   end break_thread;

   task body main_thread is
      sum : Long_Long_Integer := 0;
      count : Long_Long_Integer := 0;
   begin
      loop
         sum := sum + step;
         count := count + 1;
         exit when can_stop;
      end loop;
      delay 1.0;
      Ada.Text_IO.Put_Line("id: " & id'Img  & " sum: " & sum'Img & " count: " & count'Img);
   end main_thread;

   b1 : break_thread;
   t1 : main_thread(0, 2);
   t2 : main_thread(1, 3);
   t3 : main_thread(2, 4);
   t4 : main_thread(3, 5);
begin
   null;
end Main;
