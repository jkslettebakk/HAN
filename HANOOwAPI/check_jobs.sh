#!/bin/sh -e
#
# set -x
#
# Check if python3 jobs is running
#
var=$(ps -ef | grep [.]/HANOO)
#
# echo "Test result var :" $var
num=$(echo $var | grep -i -o "[.]/HANOO" | wc -l)
# echo "Test result num : '$num'"
#
expected_jobs=1
Date=$(date +'%Y/%m/%d at %T')
echo "-------------- Logging status at:" $Date " --------------"
#
# Check if ./HANOO is running
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
error400=$(cat /home/pi/Documents/HAN/HANOOmAPI/hanoo.log | grep "Error 400" | wc -l)
#
if  [ $error400 -ge 1 ]
then
   echo "Error 400 in communication with endpoint. Tried " $error400 " times. Rebooting. Will change to app restart /etc/./rc.local?"
   # $(/etc/./rc.local)
   # var=$(ps -ef | grep [.]/HANOO)
   # num=$(echo $var | grep -i -o "[.]/HANOO" | wc -l)
   # echo $var
   # echo "$var" | while read line; do  echo $line | awk -F " " '{print "\tJub number:" $2 }' ; done
   # echo $num " jobs running"
   sudo reboot 
else
   echo "Number of HTTP 400 error is:" $error400
fi
#
# Skal vi restarte jobbnen ve 1500 poster?
#
numLogs=$(cat /home/pi/Documents/HAN/HANOOmAPI/hanoo.log | grep "with (result.StatusCode)=OK" | wc -l)
#
if  [ $numLogs -gt 1600 ]
then
   echo "Error 400 is probably comming soon! Now we have " $numLogs " sucsessful posts. Restart app with /etc/./rc.local? Har kommentert denne ut...."
   #sudo reboot 
else
   echo "Number of HTTP OK is:" $numLogs 
fi
unhandledExcep=$(cat /home/pi/Documents/HAN/HANOOmAPI/hanoo.log | grep "Unhandled exception." | wc -l)
unhandledExcep=$(($unhadledException + $(cat /home/pi/Documents/HAN/HANOOmAPI/hanoo.log | grep "Error. Index to long. Will crash." | wc -l) ))
#
if  [ $unhandledExcep -gt 0 ]
then
   echo $unhandledExcep "Unhandled exception/Error. Will restart.... Unhandled exceptions could be system error as well."
   echo "Will logg more info in /home/pi/Documents/HAN/HANOOmAPI/hanoo.exceptions"
   $(tail -20 /home/pi/Documents/HAN/HANOOmAPI/hanoo.log >> /home/pi/Documents/HAN/HANOOmAPI/hanoo.exceptions)
   lines=$(cat /home/pi/Documents/HAN/HANOOmAPI/hanoo.exceptions | wc -l)
   echo $lines " lines logged to hanoo.exceptions."
   sudo reboot 
else
   echo $unhandledExcep "Unhandled exception. Continue."
fi
