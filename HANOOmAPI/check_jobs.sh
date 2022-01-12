#!/bin/sh -e
#
# set -x
#
# Check if python3 jobs is running
#
var=$(ps -ef | grep [.]/HANOO)
#
# echo "Test result var :" $var
num=$(echo $var | grep -i -o "./HANOO" | wc -l)
# echo "Test result num : '$num'"
#
expected_jobs=1
Date=$(date +'%Y/%m/%d at %T')
echo "Logging status at:" $Date
#
if [ $num -ge $expected_jobs ]
then
   echo "$num HANOO job(s) running. This is:"
   echo "$var" | while read line; do  echo $line | awk -F " " '{print "\t" $8 }' ; done
else
   echo "error, HANOO not found"
   echo "Few ($num) (expected >= " $expected_jobs ") ./HANOO jobs is running. Will reeboot"
   echo "$var" | while read line; do  echo $line | awk -F " " '{print "\t" $9 }' ; done
   sudo reboot 
fi
# Then test if error in logfile
#
a=$(cat /home/pi/Documents/HAN/HANOOmAPI/hanoo.log | grep "BadRequest" | wc -l)
#
if  [ $a -gt 10 ]
then
   echo "Error in communication with endpoint. Tried " $a " times. Rebooting."
   sudo reboot 
else
   echo "Number of HTTP 400 error is:" $a 
fi
