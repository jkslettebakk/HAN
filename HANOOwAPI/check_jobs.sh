#!/bin/bash
#
# set -x
#
# Define functions
haniotBackup() {
   echo "In the haniotBackup function. Logging data from haniot.log to haniot.exceptions"
   lines_before=$(cat /home/pi/Documents/HAN/HANOOwAPI/haniot.exceptions | wc -l)
   $(echo "-------------------------   " $Date "  ---------------------------------" >> /home/pi/Documents/HAN/HANOOwAPI/haniot.exceptions)
   $(tail -50 /home/pi/Documents/HAN/HANOOwAPI/haniot.log >> /home/pi/Documents/HAN/HANOOwAPI/haniot.exceptions)
   lines_after=$(cat /home/pi/Documents/HAN/HANOOwAPI/haniot.exceptions | wc -l)
   echo $(($lines_after - $lines_before)) " lines added to exception file haniot.exceptions."
}
# Check if HANIoT jobs is running
#
var=$(ps -ef | grep [.]/HANIoT)
#
# echo "Test result var :" $var
#
num=$(echo $var | grep -i -o "[.]/HANIoT" | wc -l)
#
# echo "Test result num : '$num'"
#
expected_jobs=1
Date=$(date +'%Y/%m/%d at %T')
echo "-------- Logging status at:" $Date " --------"
#
# Check if ./HANIoT is running
if [ $num -ge $expected_jobs ]
then
   echo "$num HANIoT job(s) running. This is:"
   echo "$var" | while read line; do  echo $line | cut -d ' ' -f 8- ; done
else
   echo "error, HANIoT not found"
   echo "Few ($num) (expected >= " $expected_jobs ") ./HANIoT jobs is running. Will reeboot"
   echo "$var" | while read line; do  echo $line | awk -F " " '{print "\t" $9 }' ; done
   haniotBackup # backing up data from .log file
   sudo reboot 
fi
#
BadRequest=$(cat /home/pi/Documents/HAN/HANOOwAPI/haniot.log | grep "Unhandled exception." | wc -l)
#
if  [ $BadRequest -gt 0 ] ;
then
   echo "Bad request, application (HANIoT) crash. Rebboting Pi" 
   haniotBackup # backing up data from .log file
   sudo reboot 
else
   echo "No application crash found"
   echo "Number of HTTP BadRequest error is:" $BadRequest
fi
#
#
unhandledExcep=$(($(cat /home/pi/Documents/HAN/HANOOwAPI/haniot.log | grep "Error. Index to long. Will crash." | wc -l) + $(cat /home/pi/Documents/HAN/HANOOwAPI/haniot.log | grep "Unhandled exception. " | wc -l)))
#
if  [ $unhandledExcep -gt 0 ]
then
   echo $unhandledExcep " Unhandled exception/Error. Will restart.... Unhandled exceptions could be system error as well."
   echo "Will logg more info in /home/pi/Documents/HAN/HANOOwAPI/haniot.exceptions"
   haniotBackup # backing up data from .log file
   sudo reboot 
else
   echo $unhandledExcep " Unhandled exception. Continue."
fi
