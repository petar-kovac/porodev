import dayjs from 'dayjs';
import { FC } from 'react';
import { IFilesCard, IGroupCard } from 'types/card-data';
import { ApiTranslation } from 'util/enums/api-translation-data';

interface MappedCardData {
  [key: string]: string;
}
interface EntryType {
  [key: string]: string | number | boolean;
}

const convertUnixTime = (value: string) => {
  return dayjs.unix(Date.parse(value)).format('D MMMM');
};

const mapResponseToCardData = (
  response: IGroupCard | IFilesCard,
): MappedCardData => {
  let obj: MappedCardData = {};
  // eslint-disable-next-line array-callback-return
  Object.entries(response).map(([key, value]: any) => {
    if (key === 'createdAt') {
      obj = { ...obj, [ApiTranslation.createdAt]: convertUnixTime(value) };
    } else {
      obj = {
        ...obj,
        [ApiTranslation[key]]: `${value}`,
      };
    }
  });

  return obj;
};

/**
 * Component to map trough API data.
 */
const SiderData: FC<{ data: IGroupCard | IFilesCard }> = ({ data }) => {
  const obj = mapResponseToCardData(data);

  return (
    <>
      {Object.entries(obj).map(([key, value]) => (
        <div key={key}>
          <p>
            {key}: {value}
          </p>
        </div>
      ))}
    </>
  );
};

export default SiderData;