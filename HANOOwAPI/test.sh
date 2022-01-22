#!/bin/sh -e
#
# Testing loop til IP is in place

error400=$(cat /home/pi/Documents/HAN/HANOOwAPI/hanoo.log | grep "Error 400" | wc -l)
BadRequest=$(cat /home/pi/Documents/HAN/HANOOwAPI/hanoo.log | grep "BadRequest" | wc -l)
echo $error400
echo $BadRequest
#
#  
#
if  [ $error400 -ge 10 ] || [ $BadRequest -gt 0 ] ;
then
   echo "Error 400 in communication with endpoint. Tried " $error400 " times. Rebooting. Will change to app restart /etc/./rc.local?"
else
   echo "No errors"
fi
