import dayjs from 'dayjs';

export const formatDate = (date: string) => {
  return dayjs(Date.parse(date)).format('DD/MM/YYYY hh:mm');
};
