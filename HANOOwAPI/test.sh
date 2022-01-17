#!/bin/sh -e
#
# Testing loop til IP is in place

#  $(/etc/./rc.local)
var=$(ps -ef | grep [.]/HANOO)
echo $var
num=$(echo $var | grep -i -o "[.]/HANOO" | wc -l)
echo $num
echo "$var" | while read line; do  echo $line | awk -F " " '{print "\tJub number:" $2 }' ; done 
echo " jobs running=" $num
