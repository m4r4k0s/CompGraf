import pandas as pd
import BotProcessing
from vk_api.longpoll import VkLongPoll, VkEventType


def History(bot):
    bot.RequestDataBase.to_excel('RequestDataBase.xls', index=False)

#выдаем боту ключ сообщества
bot = BotProcessing.Bot('25b554a54ef889f5dc0e8095260c043fbbab331243d1ece942afd4c631c55facd25adb42d86b5210ceca8')
#слушаем сервер
longpoll = VkLongPoll(bot.session)
for event in longpoll.listen():
    #произошлособытие, еслои это сообщение и оно адрисовано боту передаем его в обработку
    if event.type == VkEventType.MESSAGE_NEW and event.to_me:
        #передаем событие боту
        bot.EventSender(event)
        #ищим ключевые слова
        bot.KeywordSearch()
        History(bot)