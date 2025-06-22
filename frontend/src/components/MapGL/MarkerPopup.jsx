export const getMarkerPopupHTML = ({ title, description, image }) => {
    return `
    <div style="
      background: white;
      border-radius: 8px;
      box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
      overflow: hidden;
      width: 280px;
      font-family: Arial, sans-serif;
      position: relative;
    ">
      <div style="
        position: absolute;
        top: 8px;
        right: 8px;
        width: 24px;
        height: 24px;
        background: rgba(0, 0, 0, 0.5);
        border-radius: 50%;
        display: flex;
        align-items: center;
        justify-content: center;
        cursor: pointer;
        z-index: 10;
      " class="popup-close-btn">
        <svg width="12" height="12" viewBox="0 0 12 12" fill="none">
          <path d="M1 1L11 11M1 11L11 1" stroke="white" stroke-width="2" stroke-linecap="round"/>
        </svg>
      </div>

      ${image ? `
        <div style="
          height: 160px;
          background: #f5f5f5;
          overflow: hidden;
          display: flex;
          align-items: center;
          justify-content: center;
        ">
          <img src="data:image/jpeg;base64,${image}" 
               style="
                 max-width: 100%;
                 max-height: 100%;
                 object-fit: cover;
               " 
               alt="${title}"/>
        </div>
      ` : ''}
      
      <div style="padding: 16px;">
        <h3 style="
          margin: 0 0 8px 0;
          font-size: 18px;
          color: #333;
          font-weight: 600;
        ">
          ${title}
        </h3>
        
        ${description ? `
          <p style="
            margin: 0 0 16px 0;
            font-size: 14px;
            color: #666;
            line-height: 1.4;
          ">
            ${description}
          </p>
        ` : ''}
        
        <button class="details-btn" style="
          width: 100%;
          padding: 10px 0;
          background: #3b7eff;
          color: white;
          border: none;
          border-radius: 4px;
          font-size: 14px;
          font-weight: 500;
          cursor: pointer;
          transition: background 0.2s;
        ">
          Подробнее
        </button>
      </div>
    </div>
  `;
};

export const createMarkerPopup = () => {
    return `<div style="
  width: 300px;
  background: white;
  border-radius: 8px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
  padding: 16px;
  font-family: Arial, sans-serif;
">
    <div style="
        position: absolute;
        top: 8px;
        right: 8px;
        width: 24px;
        height: 24px;
        background: rgba(0, 0, 0, 0.5);
        border-radius: 50%;
        display: flex;
        align-items: center;
        justify-content: center;
        cursor: pointer;
        z-index: 10;
      " class="popup-close-btn">
        <svg width="12" height="12" viewBox="0 0 12 12" fill="none">
          <path d="M1 1L11 11M1 11L11 1" stroke="white" stroke-width="2" stroke-linecap="round"/>
        </svg>
      </div>

        <h3 style="
    margin: 0 0 16px 0;
    color: #333;
    font-size: 18px;
    font-weight: 600;
  ">
            Создать новую метку
        </h3>

        <div style="margin-bottom: 12px;">
            <label style="
      display: block;
      margin-bottom: 6px;
      color: #555;
      font-size: 14px;
    ">
                Название *
            </label>
            <input
                type="text"
                id="marker-title"
                placeholder="Например: Кафе 'Уют'"
                style="
        width: 100%;
        padding: 8px 12px;
        border: 1px solid #ddd;
        border-radius: 4px;
        font-size: 14px;
        box-sizing: border-box;
      "
                required
            />
        </div>

        <div style="margin-bottom: 16px;">
            <label style="
      display: block;
      margin-bottom: 6px;
      color: #555;
      font-size: 14px;
    ">
                Описание
            </label>
            <textarea
                id="marker-description"
                placeholder="Дополнительная информация"
                style="
        width: 100%;
        padding: 8px 12px;
        border: 1px solid #ddd;
        border-radius: 4px;
        font-size: 14px;
        min-height: 80px;
        resize: vertical;
        box-sizing: border-box;
      "
            ></textarea>
        </div>

        <button
            id="create-marker-btn"
            style="
      width: 100%;
      padding: 10px;
      background: #3b7eff;
      color: white;
      border: none;
      border-radius: 4px;
      font-size: 14px;
      font-weight: 500;
      cursor: pointer;
      transition: background 0.2s;
    "
            onMouseOver="this.style.background='#2a65d1'"
            onMouseOut="this.style.background='#3b7eff'"
        >
            Создать метку
        </button>

        <div
            style="
      position: absolute;
      top: 12px;
      right: 12px;
      width: 24px;
      height: 24px;
      display: flex;
      align-items: center;
      justify-content: center;
      cursor: pointer;
      color: #777;
      font-size: 18px;
    "
            onClick="this.closest('.dg-popup').remove()"
        >
        </div>
    </div>`
}