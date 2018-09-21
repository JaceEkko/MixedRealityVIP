import bluetooth
from BlueToothTest.py import findDevice

bd_addr = findDevice()

port = 1

sock = bluetooth.BluetoothSocket(bluetooth.RFCOMM)
sock.connect((bd_addr, port))

sock.send("Hello")

sock.close()