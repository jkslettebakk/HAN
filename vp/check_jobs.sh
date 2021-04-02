#!/bin/sh -e
#
# set -x
#
# Check if python3 jobs is running
#
var=$(ps -ef | grep [p]ython3)
#
num=$(echo $var | grep -i -o "\.py\>" | wc -l)
#

if [ $num -gt 0 ]
then
   Date=$(date +'on %D at %T')
   echo "$num python3 scripts running $Date. This is:"
   echo "$var" | while read line; do  echo $line | awk -F " " '{print "\t" $9 }' ; done
else
    echo "No ($num) .py scrips is running. Will reeboot"
    sudo reboot
fi
