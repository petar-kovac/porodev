import dayjs from 'dayjs';

export const formatDate = (date: string) => {
  return dayjs(Date.parse(date)).format('DD/MM/YYYY hh:mm');
};

export const formatDateListCard = (date: string) => {
  return dayjs(Date.parse(date)).format('DD/MM/YYYY');
};

export const millisToMinutesAndSeconds = (millis: number) => {
  const minutes = Math.floor(millis / 60000);
  const seconds: number = +((millis % 60000) / 1000).toFixed(0);
  return `${minutes}:${seconds < 10 ? '0' : ''}${seconds}`;
};
