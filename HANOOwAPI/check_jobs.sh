#!/bin/sh -e
#
# set -x
#
# Define functions
hanooBackup() {
   echo "In the hanooBackup function. Logging data from hanoo.log to hanoo.exceptions"
   $(echo "-------------------------   " $Date "  ---------------------------------" >> /home/pi/Documents/HAN/HANOOwAPI/hanoo.exceptions)
   $(tail -20 /home/pi/Documents/HAN/HANOOwAPI/hanoo.log >> /home/pi/Documents/HAN/HANOOwAPI/hanoo.exceptions)
   lines=$(cat /home/pi/Documents/HAN/HANOOwAPI/hanoo.exceptions | wc -l)
   echo $lines " lines logged to hanoo.exceptions."
}

# Check if HANOO jobs is running
#
var=$(ps -ef | grep [.]/HANOO)
#
# echo "Test result var :" $var
num=$(echo $var | grep -i -o "[.]/HANOO" | wc -l)
# echo "Test result num : '$num'"
#
expected_jobs=1
Date=$(date +'%Y/%m/%d at %T')
echo "-------- Logging status at:" $Date " --------"
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
error400=$(cat /home/pi/Documents/HAN/HANOOwAPI/hanoo.log | grep "Error 400" | wc -l)
BadRequest=$(cat /home/pi/Documents/HAN/HANOOwAPI/hanoo.log | grep "BadRequest" | wc -l)
#
if   [ $error400 -gt 1 ] || [ $BadRequest -gt 0 ] ;
then
   echo "Error 400 in communication with endpoint. Tried " $error400 " times. Will now kill, then restart app by /etc/./rc.local"
   echo "Killing prosess in HANOO job running :" $var
   jobID=$(echo $var | awk '{print $2;}') # Pull out the job ID
   echo "The job ID to be killed:" $jobID
   hanooBackup # backing up data from .log file
   sudo kill $jobID
   echo "Restarting jobs by /etc/./rc.local"
   $(/etc/./rc.local)
   # sudo reboot 
else
   echo "Number of HTTP 400 error is:" $error400
   echo "Number of HTTP BadRequest error is:" $BadRequest
fi
#
# Skal vi restarte jobbnen ved 1500 poster?
#
numLogs=$(cat /home/pi/Documents/HAN/HANOOwAPI/hanoo.log | grep " = OK" | wc -l)
#
if  [ $numLogs -gt 1600 ]
then
   echo "Error 400 is probably comming soon! Now we have " $numLogs " sucsessful posts. Restart app with /etc/./rc.local"
   #sudo reboot 
else
   echo "Number of HTTP OK is:" $numLogs 
fi
unhandledExcep=$(($(cat /home/pi/Documents/HAN/HANOOwAPI/hanoo.log | grep "Error. Index to long. Will crash." | wc -l) + $(cat /home/pi/Documents/HAN/HANOOwAPI/hanoo.log | grep "Unhandled exception." | wc -l)))
#
if  [ $unhandledExcep -gt 0 ]
then
   echo $unhandledExcep " Unhandled exception/Error. Will restart.... Unhandled exceptions could be system error as well."
   echo "Will logg more info in /home/pi/Documents/HAN/HANOOwAPI/hanoo.exceptions"
   hanooBackup # backing up data from .log file
   sudo reboot 
else
   echo $unhandledExcep "Unhandled exception. Continue."
fi
