class By:
    Id = staticmethod(lambda id: f"By.id('{id}')")
    ClassName = staticmethod(lambda class_name: f"By.className('{class_name}')")

tblWinChart = By.Id("prizes_list_slotMachine1")

class CustomizePanel:
    btnChangeBackground = By.ClassName("btnChangeBackground")
    btnChangeIcons = By.ClassName("btnChangeReels")
    btnChangeMachine = By.ClassName("btnChangeMachine")