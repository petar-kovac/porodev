import dayjs from 'dayjs';
import { FC } from 'react';
import { IFilesCard } from 'types/card-data';
import { ApiTranslation } from 'util/enums/api-translation-data';
import { formatDate } from 'util/helpers/date-formaters';

interface MappedCardData {
  [key: string]: string;
}
interface EntryType {
  [key: string]: string | number | boolean;
}

const mapResponseToCardData = (response: IFilesCard): MappedCardData => {
  let obj: MappedCardData = {};
  // eslint-disable-next-line array-callback-return
  Object.entries(response).map(([key, value]: any) => {
    if (key === 'uploadTime') {
      obj = { ...obj, [ApiTranslation.createdAt]: formatDate(value) };
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
const SiderData: FC<{ data: IFilesCard }> = ({ data }) => {
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
