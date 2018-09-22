import bluetooth

def findDevice():
    target_name = "HoloLens-FV3QH"#"HoloLens-FV3QH" HOME #"HoloLens-M3T2B" MAKERSPACE
    target_address = None

    nearby_devices = bluetooth.discover_devices()

    for bdaddr in nearby_devices:
        if target_name == bluetooth.lookup_name(bdaddr):
            target_address = bdaddr
            break

    if target_address is not None:
        print("Found Bluetooth Device " + target_address)
        return target_address
    else:
        print("Couldn't find target")
        return None
    
findDevice()